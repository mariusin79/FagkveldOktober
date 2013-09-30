using System.Web.Mvc;
using SharedContracts;

namespace FagkveldOktober.Controllers
{
    public class PurchaseController : Controller
    {
         public ActionResult View(int bookId)
         {
             return View();
         }
    }
}