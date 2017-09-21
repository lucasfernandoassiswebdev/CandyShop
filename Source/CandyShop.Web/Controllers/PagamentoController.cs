using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class PagamentoController : Controller
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
            var response = _appPagamento.ListarPagamentos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarCpf(string cpf)
        {
            var response = _appPagamento.ListarPagamentos(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarSemana()
        {
            var response = _appPagamento.ListarPagamentosSemana();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        public ActionResult ListarSemanaCpf(string cpf)
        {
            var response = _appPagamento.ListarPagamentosSemana(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro " + response.ContentAsString.First());
            return View("Index", response.Content);
        }

        #endregion

        #region Acoes

        public ActionResult InserirPagamento(PagamentoViewModel pagamento)
        {
            var response = _appPagamento.InserirPagamento(pagamento);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString.First());
            return Content("Pagamento realizado com sucesso!!");
        }        

        #endregion
    }
}