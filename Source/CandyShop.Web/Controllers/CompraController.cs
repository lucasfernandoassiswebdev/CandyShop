using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class CompraController : Controller
    {
        private readonly ICompraApplication _appCompra;
        private readonly ICompraProdutoApplication _appCompraProduto;
        private readonly IProdutoApplication _appProdutos;

        public CompraController(ICompraApplication compra, ICompraProdutoApplication compraProduto, IProdutoApplication produtos)
        {
            _appCompra = compra;
            _appCompraProduto = compraProduto;
            _appProdutos = produtos;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Editar(int idCompra)
        {
            var itens = _appCompraProduto.ListarProdutos(idCompra);
            ViewBag.Produtos = _appProdutos.ListarProdutos().Content;
            ViewBag.IdCompra = idCompra;
            var compra = _appCompra.SelecionarCompra(idCompra);
            ViewBag.Usuario = compra.Content.Usuario.Cpf;

            return View(itens.Content);
        }

        #region listas
        public ActionResult Listar()
        {
            ViewBag.tituloPagina = "Compras do ultimo mês";
            ViewBag.drop = 0;
            var response = _appCompra.ListaCompra();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarCpf()
        {
            ViewBag.tituloPagina = "Minhas Compras";
            var cpf = Session["login"].ToString();
            ViewBag.drop = 1;
            var response = _appCompra.ListaCompraPorCpf(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarSemana()
        {
            ViewBag.tituloPagina = "Compras da ultima semana";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasSemana();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarMes(int mes)
        {
            ViewBag.tituloPagina = $"Compra do mês {mes}";
            ViewBag.drop = 0;
            var response = _appCompra.ListarComprasMes(mes);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarDia()
        {
            ViewBag.tituloPagina = $"Compras do dia {DateTime.Now.ToShortDateString()}";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasDia();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }
        #endregion

        #region execuções
        [HttpPost]
        public ActionResult Cadastrar(CompraViewModel compra)
        {

            if (Session["login"].ToString() == "off")
            {
                return Content("Você não está logado");
            }

            if (ModelState.IsValid)
            {
                compra.Usuario = new UsuarioViewModel { Cpf = Session["Login"].ToString() };

                var response = _appCompra.InserirCompra(compra);

                if ((response.Status != HttpStatusCode.OK) || (response.Content < 1))
                    return Content($"Os itens da compra não puderam ser registrados: {response.ContentAsString.First()}");

                var totalCompra = _appCompra.SelecionarCompra(response.Content);
                if (totalCompra.Status != HttpStatusCode.OK)
                    return Content("Erro ao atualizar saldo" + totalCompra.ContentAsString.First());

                Session["saldoUsuario"] = Convert.ToDecimal(Session["saldoUsuario"].ToString()) - totalCompra.Content.ValorCompra;

                return Content("Sua compra foi registrada com sucesso");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Detalhes(int idCompra)
        {
            var response = _appCompra.SelecionarCompra(idCompra);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro ao detalhar compra", response.ContentAsString.First());

            return View(response.Content);
        }

        [HttpPut]
        public ActionResult Editar(CompraViewModel Compra)
        {
            var response = _appCompra.EditarCompra(Compra);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro ao editar a compra", response.ContentAsString.First());

            ViewBag.tituloPagina = "Compras do ultimo mês";
            ViewBag.drop = 0;
            var compras = _appCompra.ListaCompra();
            return View("Index", compras.Content);
        }
        #endregion
    }
}