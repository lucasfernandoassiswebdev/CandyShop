using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using CandyShop.Web.Helpers;
using System;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class CompraController : Controller
    {
        private readonly ICompraApplication _appCompra;
        private readonly IUsuarioApplication _appUsuario;

        public CompraController(ICompraApplication compra, IUsuarioApplication usuario)
        {
            _appCompra = compra;
            _appUsuario = usuario;
        }

        [UserFilterResult]
        public ActionResult Index()
        {
            return View();
        }

        #region listas
        [AdminFilterResult]
        public ActionResult Listar()
        {
            ViewBag.tituloPagina = "Compras do ultimo mês";
            ViewBag.drop = 0;
            var response = _appCompra.ListaCompra();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [UserFilterResult]
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

        [AdminFilterResult]
        public ActionResult ListarSemana()
        {
            ViewBag.tituloPagina = "Compras da ultima semana";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasSemana();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarMes(int mes)
        {
            ViewBag.tituloPagina = $"Compra do mês {mes}";
            ViewBag.drop = 0;
            var response = _appCompra.ListarComprasMes(mes);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarDia()
        {
            ViewBag.tituloPagina = $"Compras do dia {DateTime.Now.ToShortDateString()}";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasDia();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
        #endregion

        #region execuções

        [HttpPost]
        public ActionResult Cadastrar(CompraViewModel compra)
        {

            if (Session["Login"].ToString() == "off")
            {
                return Content("Efetue login e tente novamente. Você precisa estar logado para concluir sua compra");
            }

            if (ModelState.IsValid)
            {
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
            return Content("Ops... ocorreu um erro ao concluir sua compra.");
        }

        [HttpGet]
        [UserFilterResult]
        public ActionResult Detalhes(int idCompra, string paginaAnterior)
        {
            var response = _appCompra.SelecionarCompra(idCompra);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro ao detalhar compra, ", response.ContentAsString);
            var a = paginaAnterior.LastWord();
            ViewBag.endereco = Session["TipoDeLogin"].ToString().Equals("User") ?  "AjaxJsShop.voltarInicio()" : a.Count > 1 ? $"AjaxJsCompra.listarCompra{paginaAnterior.LastWord()[0]}({paginaAnterior.LastWord()[1]})" : $"AjaxJsCompra.listarCompra{paginaAnterior.LastWord()[0]}()";
            return View(response.Content);
        }

        [HttpPut]
        [AdminFilterResult]
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
        #endregion
    }
}