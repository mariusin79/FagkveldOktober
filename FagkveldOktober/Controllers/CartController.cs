using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using BooksRegistry.Contracts;
using FagkveldOktober.Cart;
using FagkveldOktober.Models;
using FagkveldOktober.ServiceGateways;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NServiceBus;
using Sales.Contracts;
using Sales.Contracts.Commands;
using SharedContracts;

namespace FagkveldOktober.Controllers
{
    public class CartController : Controller
    {
        private IBus _bus;
        private readonly IServiceGateway<ISalesService> _salesService;
        private readonly IServiceGateway<IBooksRegistryService> _booksRegistryService;

        public CartController(IBus bus, IServiceGateway<ISalesService> salesService, IServiceGateway<IBooksRegistryService> booksRegistryService)
        {
            _bus = bus;
            _salesService = salesService;
            _booksRegistryService = booksRegistryService;
        }

        public ActionResult Index()
        {
            var cart = Session.Cart();

            BookKey[] booksInCart = cart.Items.Values.Select(item => item.Book).ToArray();
            var booksInCartDetails = _booksRegistryService.Execute(service => service.GetDetailsAboutBooks(booksInCart));
            var bookPrices = _salesService.Execute(service => service.GetBooksAvailableForSale(booksInCart));

            var vm = booksInCartDetails.Select(details => new CartItemViewModel
            {
                Title = details.Title,
                Author = details.Author,
                Category = details.Category,
                Id = details.Id,
                Published = details.Published,
                Quantity = cart.Items[details.Id.Value].Quantity,
                SumTotalInOere =
                    bookPrices.First(price => price.Id == details.Id).Price.PriceInOere*
                    cart.Items[details.Id.Value].Quantity
            }).ToList();

            var json = JsonConvert.SerializeObject(vm,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            return Content(json, "application/json");
        }
        
        [HttpPut, ActionName("Index")]
        public ActionResult AddToCart(int bookId)
        {
            var cart = Session.Cart();

            cart.AddToCart(new BookKey { Value = bookId });
            Session.Cart(cart);

            return Index();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Checkout()
        {
            var cart = Session.Cart();

            _bus.Send<IPlaceAnOrder>(cmd =>
                {
                    cmd.Buyer = new CustomerKey {Value = 1};
                    cmd.BooksIAmBuying = cart.Items.Select(item => item.Value.Book).ToArray();
                });

            Session.Cart(new ShoppingCart());

            return Json(new {Status = "ok"}, JsonRequestBehavior.AllowGet);
        }
    }
}