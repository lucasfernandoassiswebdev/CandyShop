using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class AdminController : AuthController
    {
        public ActionResult Index()
        {
            Session["TipoDeLogin"] = "Admin";
            return View();
        }        
    }
}