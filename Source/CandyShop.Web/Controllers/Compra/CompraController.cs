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

        public ActionResult Listar(string token)
        {
            ViewBag.tituloPagina = "Compras do ultimo mês";
            ViewBag.drop = 0;
            var response = _appCompra.ListaCompra(token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
        public ActionResult ListarCpf(string token)
        {
            ViewBag.tituloPagina = "Minhas Compras";
            var cpf = Session["login"].ToString();
            ViewBag.drop = 1;
            var response = _appCompra.ListaCompraPorCpf(cpf, token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
        public ActionResult ListarSemana(string token)
        {
            ViewBag.tituloPagina = "Compras da ultima semana";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasSemana(token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
        public ActionResult ListarMes(int mes,string token)
        {
            ViewBag.tituloPagina = $"Compra do mês {mes}";
            ViewBag.drop = 0;
            var response = _appCompra.ListarComprasMes(mes,token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
        public ActionResult ListarDia(string token)
        {
            ViewBag.tituloPagina = $"Compras do dia {DateTime.Now.ToShortDateString()}";
            ViewBag.drop = 1;
            var response = _appCompra.ListarComprasDia(token);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }
    }
}