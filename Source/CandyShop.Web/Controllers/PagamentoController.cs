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


        // GET: Pagamento
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalhes()
        {
            return View();
        }


        //public ActionResult Listar()
        //{

        //    var response = _appPagamento.ListarPagamentosCpf();
        //    if (response.Status != HttpStatusCode.OK)
        //        return Content("Erro " + response.ContentAsString.First());
        //    return View("Index", response.Content);
        //}

        public ActionResult Inserir()
        {
            return View();
        }

        public ActionResult InserirPagamento(Pagamento pagamento)
        {
            var response = _appPagamento.InserirPagamento(pagamento);
            if (response.Status != HttpStatusCode.OK)            
                return Content("Erro" + response.ContentAsString.First());
            return Content("Pagamento realizado com sucesso!!");
        }       
    }
}