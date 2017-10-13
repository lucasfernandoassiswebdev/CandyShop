using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using CandyShop.Web.Helpers;
using System;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly IPagamentoApplication _appPagamento;
        private readonly IUsuarioApplication _appUsuario;
        public PagamentoController(IPagamentoApplication pagamento, IUsuarioApplication usuario)
        {
            _appPagamento = pagamento;
            _appUsuario = usuario;
        }

        #region Telas
        [AdminFilterResult]
        public ActionResult Index()
        {
            return View();
        }

        [UserFilterResult]
        public ActionResult Inserir()
        {
            return View();
        }

        [AdminFilterResult]
        public ActionResult Editar(int idPagamento, string paginaAnterior)
        {
            var result = _appPagamento.SelecionarPagamento(idPagamento);
            if (result.Status != HttpStatusCode.OK)
                return Content("Erro ao localizar produto");
            var a = paginaAnterior.LastWord();
            ViewBag.endereco = a.Count > 1 ? $"AjaxJsPagamento.listarPagamento{paginaAnterior.LastWord()[0]}({paginaAnterior.LastWord()[1]})" : $"AjaxJsPagamento.listarPagamento{paginaAnterior.LastWord()[0]}()";
            ViewBag.enderecoConclusao = a.Count > 1 ? $"listarPagamento{paginaAnterior.LastWord()[0]}({paginaAnterior.LastWord()[1]})" : $"listarPagamento{paginaAnterior.LastWord()[0]}()";
            return View(result.Content);
        }

        #endregion

        #region Listas

        [AdminFilterResult]
        public ActionResult Listar()
        {
            ViewBag.tituloPagina = "Pagamentos do ultimo mês";
            ViewBag.drop = 0;
            var response = _appPagamento.ListarPagamentos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [UserFilterResult]
        public ActionResult ListarCpf()
        {
            ViewBag.tituloPagina = "Meus pagamentos";
            var cpf = Session["login"].ToString();
            ViewBag.drop = 1;
            var response = _appPagamento.ListarPagamentos(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarSemana()
        {
            ViewBag.tituloPagina = $"Pagamentos da ultima semana";
            ViewBag.drop = 1;
            var response = _appPagamento.ListarPagamentosSemana();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarMes(int mes)
        {
            ViewBag.tituloPagina = $"Pagamento do mês {mes}";
            ViewBag.drop = 0;
            var response = _appPagamento.ListarPagamentos(mes);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarDia()
        {
            ViewBag.tituloPagina = $"Pagamentos do dia {DateTime.Now.ToShortDateString()}";
            ViewBag.drop = 1;
            var response = _appPagamento.ListarPagamentosDia();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("Index", response.Content);
        }

        #endregion

        #region Acoes
        [UserFilterResult]
        public ActionResult InserirPagamento(PagamentoViewModel pagamento)
        {
            pagamento.Usuario = new UsuarioViewModel { Cpf = Session["login"].ToString() };

            var response = _appPagamento.InserirPagamento(pagamento);
            if (response.Status == HttpStatusCode.OK)
            {
                var res = _appUsuario.SelecionarUsuario(Session["login"].ToString());
                if (response.Status != HttpStatusCode.OK)
                    return Content("Erro ao atualizar saldo, " + response.ContentAsString);
                Session["saldoUsuario"] = $"{res.Content.SaldoUsuario:C}";
            }
            else return Content("Erro. " + response.ContentAsString);
            return Content("Pagamento realizado com sucesso!!");
        }

        [AdminFilterResult]
        public ActionResult EditarPagamento(PagamentoViewModel pagamento)
        {
            var response = _appPagamento.EditarPagamento(pagamento);

            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            if (!Session["Login"].ToString().Equals(pagamento.Usuario.Cpf))
                return Content("Pagamento editado com sucesso!!");
            var user = _appUsuario.SelecionarUsuario(pagamento.Usuario.Cpf);
            if (user.Status != HttpStatusCode.OK)
                return Content("Erro ao atualizar seu saldo");
            Session["saldoUsuario"] = $"{user.Content.SaldoUsuario:C}";

            return Content("Pagamento editado com sucesso!!");
        }
        #endregion
    }
}