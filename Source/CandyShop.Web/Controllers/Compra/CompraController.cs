using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers.Compra
{
    [AdminFilterResult]
    public class CompraController : CompraUserComumController
    {
        private readonly ICompraApplication _appCompra;
        private readonly IUsuarioApplication _appUsuario;

        public CompraController(ICompraApplication compra, IUsuarioApplication usuario) : base(compra)
        {
            _appCompra = compra;
            _appUsuario = usuario;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            ViewBag.tituloPagina = "Compras do ultimo mês";
            ViewBag.drop = 0;
            var response = _appCompra.ListaCompra();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [HttpGet]
        public ActionResult ListarCpf()
        {
            ViewBag.tituloPagina = "Minhas Compras";
            var cpf = Session["login"].ToString();
            ViewBag.drop = 1;
            var response = _appCompra.ListaCompraPorCpf(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [HttpGet]
        public ActionResult ListarSemana()
        {
            ViewBag.tituloPagina = "Compras da ultima semana";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasSemana();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [HttpGet]
        public ActionResult ListarMes(int mes)
        {
            ViewBag.tituloPagina = $"Compra do mês {mes}";
            ViewBag.drop = 0;
            var response = _appCompra.ListarComprasMes(mes);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [HttpGet]
        public ActionResult ListarDia()
        {
            ViewBag.tituloPagina = $"Compras do dia {DateTime.Now.ToShortDateString()}";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasDia();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [HttpPost]
        public ActionResult Cadastrar(CompraViewModel compra)
        {

            if (Session["Login"].ToString() == "off")
                return Content("Efetue login e tente novamente. Você precisa estar logado para concluir sua compra");

            if (!ModelState.IsValid) return Content("Ops... ocorreu um erro ao concluir sua compra.");
            compra.Usuario = new UsuarioViewModel { Cpf = Session["Login"].ToString() };

            var response = _appCompra.InserirCompra(compra);

            if ((response.Status != HttpStatusCode.OK) || (response.Content < 1))
                return Content($"Os itens da compra não puderam ser registrados: {response.ContentAsString  }");

            var user = _appUsuario.SelecionarUsuario(Session["Login"].ToString());
            if (user.Status != HttpStatusCode.OK)
                return Content($"Erro ao atualizar seu saldo, {user.ContentAsString}");
            Session["saldoUsuario"] = $"{user.Content.SaldoUsuario:C}";
            TempData["LimparCarrinho"] = true;
            return Content("Sua compra foi registrada com sucesso");
        }

        public ActionResult Editar(CompraViewModel Compra)
        {
            var response = _appCompra.EditarCompra(Compra);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro ao editar a compra, ", response.ContentAsString);

            ViewBag.tituloPagina = "Compras do ultimo mês";
            ViewBag.drop = 0;
            var compras = _appCompra.ListaCompra();
            return View("Index", compras.Content);
        }
    }
}