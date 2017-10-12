using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoApplication _appProduto;
        private readonly IUsuarioApplication _appUsuario;
        private readonly string _pathProduto;

        public HomeController(IProdutoApplication produto, IUsuarioApplication usuario)
        {
            _appProduto = produto;
            _appUsuario = usuario;
            _pathProduto = "Imagens/Produtos";
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
            if (!response.IsSuccessStatusCode)
                return Content("1"); //Content("CPF ou senha incorretos!");

            var model = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);

            if (model != 1)
            {
                return Content("1");
            }
            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            var user = _appUsuario.SelecionarUsuario(cpf);
            if (user.Status != HttpStatusCode.OK)
                return Content($"Erro ao resgatar dados. {user.ContentAsString}");

            Session["classificacao"] = user.Content.Classificacao;
            Session["nomeUsuario"] = user.Content.NomeUsuario;
            Session["saldoUsuario"] = $"{user.Content.SaldoUsuario:C}";
            Session["Login"] = user.Content.Cpf.Replace(".", "").Replace("-", "");
            return View("NavBar");
        }
    }
}