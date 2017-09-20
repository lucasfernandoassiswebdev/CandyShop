using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Padrao()
        {
            if(Session["Login"] == null || Session["Login"].ToString() != "logado")
                Session["Login"] = "deslogado";

            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
    }
}