using System.Web.Mvc;

namespace FagkveldOktober.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Widget()
        {
            return PartialView();
        }
    }
}