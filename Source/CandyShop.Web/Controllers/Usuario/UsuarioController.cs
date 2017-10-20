using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers.Usuario
{
    [AdminFilterResult]
    public class UsuarioController : UsuarioComumController
    {
        private readonly IUsuarioApplication _appUsuario;

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
        public ActionResult Editar(string cpf, string telaAnterior)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);

            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro. " + usuario.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(usuario.Content);
        }
        public ActionResult Desativar(string cpf, string telaAnterior)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);
            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro. " + usuario.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(usuario.Content);
        }
        public ActionResult Detalhes(string cpf, string telaAnterior)
        {
            var response = _appUsuario.SelecionarUsuario(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            ViewBag.telaAnterior = telaAnterior;
            return View(response.Content);
        }

        public ActionResult Listar()
        {
            var response = _appUsuario.ListarUsuarios();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários Ativos";
            return View("Index", response.Content);
        }
        public ActionResult ListarUsuariosEmDivida()
        {
            var response = _appUsuario.ListarUsuariosEmDivida();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários em Dívida";
            return View("Index", response.Content);
        }
        public ActionResult ListarInativos()
        {
            var response = _appUsuario.ListarInativos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["nomeLista"] = "Usuários Inativos";
            return View("Index", response.Content);
        }
        public ActionResult ProcurarUsuario(string nome)
        {
            var response = _appUsuario.ProcurarUsuario(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            TempData["nomeLista"] = "Usuários Relacionados";
            return View("Index", response.Content);
        }

        [HttpPost]
        public ActionResult Cadastrar(UsuarioViewModel usuario)
        {
            if (usuario.Cpf == null || usuario.NomeUsuario == null)
                return Content("Os campos de CPF e Nome são obrigatórios para o cadastro!");

            if (!ModelState.IsValid) return Content("Ops, ocorreu um erro ao editar usuário.");

            var response = _appUsuario.InserirUsuario(usuario);

            return Content(response.Status != HttpStatusCode.OK
                ? $"{response.ContentAsString}"
                : response.Content);
        }
        [HttpPost]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            if (usuario.Cpf == null || usuario.NomeUsuario == null || usuario.Classificacao == null ||
                usuario.Ativo == null || usuario.SenhaUsuario == null)
                return Content("Preencha todos os campos");

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
        [HttpPost]
        public ActionResult DesativarUsuario(UsuarioViewModel usuario)
        {
            var response = _appUsuario.DesativarUsuario(usuario);
            return Content(response.Status != HttpStatusCode.OK ? $"Erro {response.Status}" : "Usuario desativado com sucesso!");
        }
    }
}