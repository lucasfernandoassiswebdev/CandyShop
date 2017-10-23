using CandyShop.Web.Filters;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    [AdminFilterResult]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            Session["TipoDeLogin"] = "Admin";
            return View();
        }
    }
}