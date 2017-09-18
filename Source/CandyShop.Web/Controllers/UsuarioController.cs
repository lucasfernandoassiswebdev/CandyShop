using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApplication _appUsuario;

        public UsuarioController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
        }

        public ActionResult Index()
        {
            var response = _appUsuario.ListarUsuarios();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString);
            }
            return View(response.Content);
        }

        public ActionResult ListarUsuariosEmDivida()
        {
            var response = _appUsuario.ListarUsuariosEmDivida();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString);
            }
            return View(response.Content);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Usuario usuario)
        {
            var response = _appUsuario.InserirUsuario(usuario);
            if (response.Status != HttpStatusCode.OK)
                return Content("Deu ruim!");
            return Content("Deu bom!!");
        }

        public ActionResult Detalhes(string cpf)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);

            if (usuario == null)
                return HttpNotFound();

            return View(usuario);
        }

        public ActionResult Editar()
        {
            return View();
        }
    }
}