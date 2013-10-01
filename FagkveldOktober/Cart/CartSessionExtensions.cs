using System.Web;

namespace FagkveldOktober.Cart
{
    public static class CartSessionExtensions
    {
        public static ShoppingCart Cart(this HttpSessionStateBase session)
        {
            return (ShoppingCart)session["Cart"] ?? new ShoppingCart();            
        }

        public static void Cart(this HttpSessionStateBase session, ShoppingCart cart)
        {
            session["Cart"] = cart;
        }
    }
}