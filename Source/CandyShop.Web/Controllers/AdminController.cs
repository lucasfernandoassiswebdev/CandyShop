using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Login"] == null)
                Session["Login"] = "off";
            return View();
        }
        
    }
}