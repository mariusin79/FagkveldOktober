using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BooksRegistry.Contracts;
using FagkveldOktober.Cart;
using FagkveldOktober.Models;
using FagkveldOktober.ServiceGateways;
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

        public ActionResult Widget()
        {
            return PartialView();
        }

        public ViewResult Cart()
        {
            var vm = new CartViewModel {Content = new List<CartItemViewModel>()};

            var cart = Session.Cart();

            BookKey[] booksInCart = cart.Items.Values.Select(item => item.Book).ToArray();
            var booksInCartDetails = _booksRegistryService.Execute(service => service.GetDetailsAboutBooks(booksInCart));
            var bookPrices = _salesService.Execute(service => service.GetBooksAvailableForSale(booksInCart));

            foreach (var details in booksInCartDetails)
            {
                vm.Content.Add(new CartItemViewModel
                    {
                        Title = details.Title,
                        Author = details.Author, 
                        Category = details.Category, 
                        Id = details.Id, 
                        Published = details.Published, 
                        Quantity = cart.Items[details.Id.Value].Quantity, 
                        SumTotalInOere = bookPrices.First(price => price.Id == details.Id).Price.PriceInOere * cart.Items[details.Id.Value].Quantity
                    });
            }

            vm.SumTotalInOere = vm.Content.Sum(item => item.SumTotalInOere);

            return View(vm);
        }

        public ActionResult Checkout()
        {
            var cart = Session.Cart();

            _bus.Send<IPlaceAnOrder>(cmd =>
                {
                    cmd.Buyer = new CustomerKey {Value = 1};
                    cmd.BooksIAmBuying = cart.Items.Select(item => item.Value.Book).ToArray();
                });

            Session.Cart(new ShoppingCart());

            return RedirectToAction("Index", "Home");
        }
    }
}