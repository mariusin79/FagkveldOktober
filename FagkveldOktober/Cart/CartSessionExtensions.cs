using System.Web.SessionState;

namespace FagkveldOktober.Cart
{
    public static class CartSessionExtensions
    {
        public static ShoppingCart Cart(this HttpSessionState session)
        {
            return (ShoppingCart)session["Cart"] ?? new ShoppingCart();            
        }

        public static void Cart(this HttpSessionState session, ShoppingCart cart)
        {
            session["Cart"] = cart;
        }
    }
}