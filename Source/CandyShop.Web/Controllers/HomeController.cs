using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NavBar()
        {
            if (Session["Login"] == null)
                Session["Login"] = "off";
            Session["TipoDeLogin"] = "User";           
            return View();
        }
    }
}