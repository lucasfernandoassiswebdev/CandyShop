using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoApplication _appProduto;
        private readonly IUsuarioApplication _appUsuario;
        private readonly ICompraApplication _appCompra;
        private readonly string _pathProduto;

        public HomeController(IProdutoApplication produto, IUsuarioApplication usuario, ICompraApplication compra)
        {
            _appProduto = produto;
            _appUsuario = usuario;
            _pathProduto = "Imagens/Produtos";
            _appCompra = compra;
        }

        public ActionResult NavBar()
        {
            if (Session["Login"] == null)
                Session["Login"] = "off";
            Session["TipoDeLogin"] = "User";
            return View();
        }
        public ActionResult GridProdutos()
        {
            return View();
        }

        public ActionResult Index()
        {
            var response = _appProduto.ListarProdutos();
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro. " + response.ContentAsString);

            TempData["caminhoImagensProdutos"] = _pathProduto;
            return View("GridProdutos", response.Content);
        }
        public ActionResult ProcurarProduto(string nome)
        {
            var response = _appProduto.ProcurarProduto(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.ContentAsString}");

            ViewBag.Pesquisa = nome;
            TempData["caminhoImagensProdutos"] = _pathProduto;
            return View("GridProdutos", response.Content);
        }
        public ActionResult ListarCategoria(string categoria)
        {
            var response = _appProduto.ListarCategoria(categoria);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Content}");

            ViewBag.Pesquisa = categoria;
            TempData["caminhoImagensProdutos"] = _pathProduto;
            return View("GridProdutos", response.Content);
        }

        [HttpPost]
        public ActionResult Logar(UsuarioViewModel usuario)
        {
            var response = _appUsuario.VerificaLogin(usuario);
            if (response.Status != HttpStatusCode.OK)
                return Content(response.Content);

            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            var user = _appUsuario.SelecionarUsuario(cpf);
            if (user.Status != HttpStatusCode.OK)
                return Content($"Erro ao resgatar dados. {user.ContentAsString}");

            Session["classificacao"] = user.Content.Classificacao;
            Session["nomeUsuario"] = user.Content.NomeUsuario.Split(null)[0];
            Session["saldoUsuario"] = $"{user.Content.SaldoUsuario:C}";
            Session["Login"] = user.Content.Cpf.Replace(".", "").Replace("-", "");
            return Content(response.Content);
        }
        [HttpPost]
        public ActionResult Cadastrar(CompraViewModel compra)
        {

            if (Session["Login"].ToString() == "off")
                return Content("Efetue login e tente novamente. Você precisa estar logado para concluir sua compra");

            if (!ModelState.IsValid) return Content("Ops... ocorreu um erro ao concluir sua compra.");
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
    }
}