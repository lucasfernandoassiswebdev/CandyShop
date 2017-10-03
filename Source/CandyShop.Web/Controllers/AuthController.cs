using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class AuthController : Controller
    {
        //Método que autentica o login, se a session Login estiver nula retorna um content com uma string
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            if (Session["Login"].ToString() == "off")
                filterContext.Result = new RedirectResult("/Home/NavBar");
            base.OnActionExecuting(filterContext);
        }
    }
}