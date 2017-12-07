using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using PagedList;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers.Usuario
{
    [AdminFilterResult]
    public class UsuarioController : UsuarioComumController
    {
        private readonly IUsuarioApplication _appUsuario;
        private const int TamanhoPagina = 5;

        public UsuarioController(IUsuarioApplication usuario) : base(usuario)
        {
            _appUsuario = usuario;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }
        public ActionResult Editar(string cpf, string telaAnterior, string token)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf, token);

            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro. " + usuario.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(usuario.Content);
        }
        public ActionResult Desativar(string cpf, string telaAnterior,string token)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf, token);
            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro. " + usuario.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(usuario.Content);
        }
        public ActionResult Detalhes(string cpf, string telaAnterior, string token)
        {
            var response = _appUsuario.SelecionarUsuario(cpf, token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(response.Content);
        }

        public ActionResult Listar(string token)
        {
            var response = _appUsuario.ListarUsuarios(token);

            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários Ativos";
            return View("Index", response.Content);
        }
        public ActionResult ListarUsuariosEmDivida(string token)
        {
            var response = _appUsuario.ListarUsuariosEmDivida(token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários em Dívida";
            return View("Index", response.Content);
        }
        public ActionResult ListarInativos(string token)
        {
            var response = _appUsuario.ListarInativos(token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários Inativos";
            return View("Index", response.Content);
        }
        public ActionResult ProcurarUsuario(string nome, string token)
        {
            var response = _appUsuario.ProcurarUsuario(nome, token);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            TempData["nomeLista"] = "Usuários Relacionados";
            return View("Index", response.Content);
        }

       
        [HttpPost]
        public ActionResult Cadastrar(UsuarioViewModel usuario, string token)
        {
            if (usuario.Cpf == null || usuario.NomeUsuario == null)
                return Content("Os campos de CPF e Nome são obrigatórios para o cadastro!");

            if (!ModelState.IsValid) return Content("Ops, ocorreu um erro ao editar usuário.");

            var response = _appUsuario.InserirUsuario(usuario, token);

            if(response.Status != HttpStatusCode.OK)
                return Content($"{response.ContentAsString}");

            return Content("Cadastro feito com sucesso!");
        }
        [HttpPost]
        public ActionResult Editar(UsuarioViewModel usuario, string token)
        {
            if (usuario.Cpf == null || usuario.NomeUsuario == null || usuario.Classificacao == null ||
                usuario.Ativo == null || usuario.SenhaUsuario == null || usuario.Email == null)
                return Content("Preencha todos os campos");

            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            var response = _appUsuario.EditarUsuario(usuario, token);

            if (response.Status != HttpStatusCode.OK && response.Status != HttpStatusCode.NotModified)
                return Content("Erro. " + response.ContentAsString);

            if (!Session["Login"].ToString().Equals(cpf)) return Content("Edição concluída com sucesso!!");

            var res = _appUsuario.SelecionarUsuario(cpf, token);
            if (res.Status != HttpStatusCode.OK)
                return Content("Erro ao atualizar seu saldo");

            Session["saldoUsuario"] = res.Content.SaldoUsuario;
            return Content("Edição concluída com sucesso!!");
        }
        [HttpPost]
        public ActionResult DesativarUsuario(UsuarioViewModel usuario, string token)
        {
            var response = _appUsuario.DesativarUsuario(usuario, token);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro {response.Status}" : "Usuario desativado com sucesso!");
        }
    }
}
