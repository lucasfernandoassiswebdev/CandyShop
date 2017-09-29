using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Login"].ToString() == "off")
                Session["Login"] = "admin";
            return View();
        }
        
    }
}