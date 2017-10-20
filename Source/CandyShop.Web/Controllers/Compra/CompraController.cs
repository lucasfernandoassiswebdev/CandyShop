using CandyShop.Application.Interfaces;
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

        public CompraController(ICompraApplication compra, IUsuarioApplication usuario) : base(compra)
        {
            _appCompra = compra;
        }

        public ActionResult Listar()
        {
            ViewBag.tituloPagina = "Compras do ultimo mês";
            ViewBag.drop = 0;
            var response = _appCompra.ListaCompra();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
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
        public ActionResult ListarSemana()
        {
            ViewBag.tituloPagina = "Compras da ultima semana";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasSemana();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
        public ActionResult ListarMes(int mes)
        {
            ViewBag.tituloPagina = $"Compra do mês {mes}";
            ViewBag.drop = 0;
            var response = _appCompra.ListarComprasMes(mes);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
        public ActionResult ListarDia()
        {
            ViewBag.tituloPagina = $"Compras do dia {DateTime.Now.ToShortDateString()}";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasDia();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
    }
}