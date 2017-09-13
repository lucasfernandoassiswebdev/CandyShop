using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}