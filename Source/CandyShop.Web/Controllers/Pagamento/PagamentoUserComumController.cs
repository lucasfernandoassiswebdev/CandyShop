using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers.Pagamento
{
    [UserFilterResult]
    public class PagamentoUserComumController : Controller
    {
        private readonly IPagamentoApplication _appPagamento;
        private readonly IUsuarioApplication _appUsuario;

        public PagamentoUserComumController(IPagamentoApplication pagamento, IUsuarioApplication usuario)
        {
            _appPagamento = pagamento;
            _appUsuario = usuario;
        }

        
        public ActionResult Inserir()
        {
            return View("../Pagamento/Inserir");
        }

        public ActionResult ListarCpf()
        {
            ViewBag.tituloPagina = "Meus pagamentos";
            var cpf = Session["login"].ToString();
            ViewBag.drop = 1;
            var response = _appPagamento.ListarPagamentos(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);
            return View("../Pagamento/Index", response.Content);
        }
        
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
    }
}