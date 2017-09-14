using CandyShop.Core.Services.CompraProduto;
using System.Web.Mvc;

namespace CandyShop.WebAPI.Controllers
{
    public class CompraProdutoController : Controller
    {
        private readonly ICompraProdutoRepository _compraProdutoRepository;

        public CompraProdutoController(ICompraProdutoRepository compraProdutoRepository)
        {
            _compraProdutoRepository = compraProdutoRepository;
        }

        // GET: CompraProduto
        public ActionResult Index()
        {
            return View();
        }
    }
}