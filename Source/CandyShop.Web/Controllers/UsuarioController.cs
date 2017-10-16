using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApplication _appUsuario;
        private readonly string _pathUsuario;
        private readonly string _pathUsuarioSemTio;
        public UsuarioController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
            _pathUsuario = "http://189.112.203.1:45000/candyShop/usuarios";
        }

        #region telas
        [AdminFilterResult]
        public ActionResult Index()
        {
            return View();
        }

        [AdminFilterResult]
        public ActionResult Cadastrar()
        {
            TempData["caminhoImagensUsuarios"] = _pathUsuario;
            return View();
        }

        [AdminFilterResult]
        public ActionResult Editar(string cpf, string telaAnterior)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);

            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro. " + usuario.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensUsuarios"] = _pathUsuario;
            return View(usuario.Content);
        }

        [AdminFilterResult]
        public ActionResult Desativar(string cpf, string telaAnterior)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);
            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro. " + usuario.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensUsuarios"] = _pathUsuario;
            return View(usuario.Content);
        }

        [AdminFilterResult]
        public ActionResult Detalhes(string cpf, string telaAnterior)
        {
            var response = _appUsuario.SelecionarUsuario(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensUsuarios"] = _pathUsuario;
            return View(response.Content);
        }
        #endregion

        #region listas

        [AdminFilterResult]
        public ActionResult Listar()
        {
            var response = _appUsuario.ListarUsuarios();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários Ativos";
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarUsuariosEmDivida()
        {
            var response = _appUsuario.ListarUsuariosEmDivida();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários em Dívida";
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarInativos()
        {
            var response = _appUsuario.ListarInativos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários Inativos";
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ProcurarUsuario(string nome)
        {
            var response = _appUsuario.ProcurarUsuario(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            TempData["nomeLista"] = "Usuários Relacionados";
            return View("Index", response.Content);
        }
        #endregion

        #region EXECUCOES
        [AdminFilterResult]
        [HttpPost]
        public ActionResult Cadastrar(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid) return Content("Ops, ocorreu um erro ao editar usuário.");

            var response = _appUsuario.InserirUsuario(usuario);

            return Content(response.Status != HttpStatusCode.OK ? $"{response.ContentAsString}" : "Usuário cadastrado com sucesso");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            var response = _appUsuario.EditarUsuario(usuario);

            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            if (!Session["Login"].ToString().Equals(cpf)) return Content("Edição concluída com sucesso!!");

            var res = _appUsuario.SelecionarUsuario(cpf);
            if (res.Status != HttpStatusCode.OK)
                return Content("Erro ao atualizar seu saldo");
            Session["saldoUsuario"] = res.Content.SaldoUsuario;

            return Content("Edição concluída com sucesso!!");
        }

        [UserFilterResult]
        public ActionResult TrocarSenha(TrocaSenhaViewModel senhas)
        {
            if (!senhas.ConfirmaNovaSenha.Equals(senhas.NovaSenha)) return Content("Senhas não conferem");
            var usuario = new UsuarioViewModel
            {
                Cpf = Session["Login"].ToString(),
                SenhaUsuario = senhas.NovaSenha
            };
            var response = _appUsuario.TrocarSenha(usuario);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro ao trocar a senha, {response.ContentAsString}" : "Senha atualizada com sucesso");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult DesativarUsuario(UsuarioViewModel usuario)
        {
            var response = _appUsuario.DesativarUsuario(usuario);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro {response.Status}" : "Usuario desativado com sucesso!");
        }

        [UserFilterResult]
        public ActionResult Deslogar()
        {
            Session.Clear();
            return RedirectToAction("NavBar", "Home");
        }
        #endregion
    }
}