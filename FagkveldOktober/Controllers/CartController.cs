using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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
    public class CartController : ApiController
    {
        private readonly IBus _bus;
        private readonly IServiceGateway<ISalesService> _salesService;
        private readonly IServiceGateway<IBooksRegistryService> _booksRegistryService;

        public CartController(IBus bus, IServiceGateway<ISalesService> salesService, IServiceGateway<IBooksRegistryService> booksRegistryService)
        {
            _bus = bus;
            _salesService = salesService;
            _booksRegistryService = booksRegistryService;
        }

        // GET api/cart
        public IEnumerable<CartItemViewModel> Get()
        {
            var cart = HttpContext.Current.Session.Cart();

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
                    bookPrices.First(price => price.Id == details.Id).Price.PriceInOere *
                    cart.Items[details.Id.Value].Quantity
            }).ToList();
            return vm;
        }


        public void Post()
        {
            var cart = HttpContext.Current.Session.Cart();

            _bus.Send<IPlaceAnOrder>(cmd =>
            {
                cmd.Buyer = new CustomerKey { Value = 1 };
                cmd.BooksIAmBuying = cart.Items.Select(item => item.Value.Book).ToArray();
            });

            HttpContext.Current.Session.Cart(new ShoppingCart());
        }

        public IEnumerable<CartItemViewModel> Put([FromBody]dynamic body)
        {
            var cart = HttpContext.Current.Session.Cart();

            cart.AddToCart(new BookKey { Value = body.bookId });
            HttpContext.Current.Session.Cart(cart);
            return Get();
        }
    }
}
