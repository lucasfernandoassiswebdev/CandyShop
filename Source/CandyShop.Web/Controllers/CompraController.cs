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

        public CompraController(ICompraApplication compra)
        {
            _appCompra = compra;
        }

        // GET: Compra
        public ActionResult Index()
        {
            return View();
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
                compra.Usuario = new UsuarioViewModel();
                compra.Usuario.Cpf = Session["Login"].ToString();

                var response = _appCompra.InserirCompra(compra);

                var ultimaCompra = _appCompra.VerificarUltimaCompra().Content;
                if (ultimaCompra == 0)
                        return Content($"Os itens da compra não puderam ser registrados: {response.Status}");

                foreach (var produto in compra.Itens)
                {
                    produto.IdProduto = ultimaCompra;
                    _appCompra.InserirItens(produto);
                }

                if (response.Status != HttpStatusCode.OK)
                    return Content($"Não foi possível registrar sua compra: {response.Status}");

                return Content("Sua compra foi registrada com sucesso");
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}