using System.Linq;
using System.Web.Mvc;
using FagkveldOktober.Cart;
using NServiceBus;
using Sales.Contracts.Commands;
using SharedContracts;

namespace FagkveldOktober.Controllers
{
    public class CartController : Controller
    {
        private IBus _bus;

        public CartController(IBus bus)
        {
            _bus = bus;
        }

        public ActionResult Widget()
        {
            return PartialView();
        }

        public ViewResult Cart()
        {
            return View(Session.Cart());
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