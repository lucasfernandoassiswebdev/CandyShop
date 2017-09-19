using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Linq;
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
                return Content("Erro " + response.ContentAsString.First());
            }
            return View(response.Content);
        }

        public ActionResult ListarUsuariosEmDivida()
        {
            var response = _appUsuario.ListarUsuariosEmDivida();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString.First());
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

            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro " + usuario.ContentAsString.First());
            
            return View(usuario.Content);
        }

        public ActionResult Editar(string cpf)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);

            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro " + usuario.ContentAsString.First());

            return View(usuario.Content);
        }

        [HttpPost]        
        public ActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var response = _appUsuario.EditarUsuario(usuario);

                if (response.Status != HttpStatusCode.OK)
                    return Content("Erro " + response.ContentAsString.First());

                return Content("Edição concluída com sucesso!!");

            }

            ModelState.AddModelError("USUARIO","Formulário inválido");
            return View("Editar");
        }
    }
}