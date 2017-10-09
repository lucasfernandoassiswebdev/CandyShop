using CandyShop.Web.Filters;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class AdminController : Controller
    {
        [AdminFilterResult]
        public ActionResult Index()
        {
            Session["TipoDeLogin"] = "Admin";
            return View();
        }
    }
}