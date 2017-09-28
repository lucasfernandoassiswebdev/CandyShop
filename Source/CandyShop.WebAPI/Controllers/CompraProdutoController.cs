using CandyShop.Core.Services;
using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.CompraProduto.Dto;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class CompraProdutoController : ApiController
    {
        private readonly ICompraProdutoRepository _compraProdutoRepository;
        
        private readonly INotification _notification;
        
        public CompraProdutoController(ICompraProdutoRepository compraProdutoRepository, INotification notification)
        {
            _notification = notification;
            _compraProdutoRepository = compraProdutoRepository;            
        }

        //public IHttpActionResult Post(CompraProdutoDto compraProduto)
        //{
        //    try
        //    {
        //        VerificaEstoque(compraProduto);
        //        if (_notification.HasNotification())
        //            return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

        //        _compraProdutoRepository.InserirCompraProduto(compraProduto);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.NotAcceptable, e.Message);
        //    }
        //}

        public IHttpActionResult Get()
        {
            return Ok(_compraProdutoRepository.ListarCompraProduto());
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(_compraProdutoRepository.ListarCompraProdutoIdVenda(id));
        }

        public IHttpActionResult Put(CompraProdutoDto compraProduto)
        {
            _compraProdutoRepository.EditarCompraProduto(compraProduto);
            return Ok();
        }

        
    }
}