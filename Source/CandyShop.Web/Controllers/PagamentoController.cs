﻿using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class PagamentoController : AuthController
    {
        private readonly IPagamentoApplication _appPagamento;

        public PagamentoController(IPagamentoApplication pagamento)
        {
            _appPagamento = pagamento;
        }


        #region Telas

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalhes()
        {
            return View();
        }

        public ActionResult Inserir()
        {
            return View();
        }

        #endregion

        #region Listas

        public ActionResult Listar()
        {
            ViewBag.tituloPagina = "Pagamentos do ultimo mês";
            ViewBag.drop = 0;
            var response = _appPagamento.ListarPagamentos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarCpf()
        {
            ViewBag.tituloPagina = "Meus pagamentos";
            var cpf = Session["login"].ToString();
            ViewBag.drop = 1;
            var response = _appPagamento.ListarPagamentos(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarSemana()
        {
            ViewBag.tituloPagina = $"Pagamentos da ultima semana";
            ViewBag.drop = 1;
            var response = _appPagamento.ListarPagamentosSemana();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarMes(int mes)
        {
            ViewBag.tituloPagina = $"Pagamento do mês {mes}";
            ViewBag.drop = 0;
            var response = _appPagamento.ListarPagamentos(mes);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarDia()
        {
            ViewBag.tituloPagina = $"Pagamentos do dia {DateTime.Now.ToShortDateString()}";
            ViewBag.drop = 1;
            var response = _appPagamento.ListarPagamentosDia();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        #endregion

        #region Acoes

        public ActionResult InserirPagamento(PagamentoViewModel pagamento)
        {
            pagamento.Usuario = new UsuarioViewModel { Cpf = Session["login"].ToString() };
            Session["saldoUsuario"] = Convert.ToDecimal(Session["saldoUsuario"].ToString()) + pagamento.ValorPagamento;

            var response = _appPagamento.InserirPagamento(pagamento);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return Content("Pagamento realizado com sucesso!!");
        }        

        #endregion
    }
}