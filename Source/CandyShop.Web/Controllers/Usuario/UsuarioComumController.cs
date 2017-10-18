using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers.Usuario
{
    [UserFilterResult]
    public class UsuarioComumController : Controller
    {
        private readonly IUsuarioApplication _appUsuario;

        public UsuarioComumController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
        }

        public ActionResult Deslogar()
        {
            Session.Clear();
            return RedirectToAction("NavBar", "Home");
        }

        public ActionResult TrocarSenha(TrocaSenhaViewModel senhas)
        {
            if (!senhas.ConfirmaNovaSenha.Equals(senhas.NovaSenha)) return Content("Senhas não conferem");
            var usuario = new UsuarioViewModel
            {
                Cpf = Session["Login"].ToString(),
                SenhaUsuario = senhas.NovaSenha
            };
            var response = _appUsuario.TrocarSenha(usuario);
            return Content(response.Status != HttpStatusCode.OK
                ? $"Erro ao trocar a senha, {response.ContentAsString}"
                : "Senha atualizada com sucesso");
        }
    }
}