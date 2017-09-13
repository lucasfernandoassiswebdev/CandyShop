using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }
    }
}