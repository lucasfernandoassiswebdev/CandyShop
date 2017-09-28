using CandyShop.Core.Services;
using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.CompraProduto.Dto;
using CandyShop.Core.Services.Produto;
using System;
using System.Net;
using System.Web.Http;
using CandyShop.Core.Services.Produto.Dto;

namespace CandyShop.WebAPI.Controllers
{
    public class CompraProdutoController : ApiController
    {
        private readonly ICompraProdutoRepository _compraProdutoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly INotification _notification;
        
        public CompraProdutoController(ICompraProdutoRepository compraProdutoRepository, INotification notification, IProdutoRepository produtoRepository)
        {
            _notification = notification;
            _compraProdutoRepository = compraProdutoRepository;
            _produtoRepository = produtoRepository;
        }

        public IHttpActionResult Post(CompraProdutoDto compraProduto)
        {
            try
            {
                VerificaEstoque(compraProduto);
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

                //if (VerificaEstoque(compraProduto.QtdeCompra,compraProduto.Produto.IdProduto))
                //    return BadRequest("Quantidade da compra indisponível no estoque!");

                _compraProdutoRepository.InserirCompraProduto(compraProduto);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

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

        private void VerificaEstoque(CompraProdutoDto item)
        {
            var estoque = _produtoRepository.SelecionarDadosProduto(item.Produto.IdProduto).QtdeProduto;
            if (item.QtdeCompra > estoque)
                _notification.Add("Quantidade da compra indisponível no estoque!");            
        }
    }
}