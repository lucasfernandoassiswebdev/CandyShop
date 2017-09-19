using CandyShop.Core.Services;
using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Produto.Dto;
using System;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly INotification _notification;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;


        public ProdutoController(INotification notification, IProdutoRepository produtoRepository, IProdutoService produtoService)
        {
            _notification = notification;
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
        }

        public IHttpActionResult Post(ProdutoDto produto)
        {
            try
            {
                if (_notification.HasNotification())
                    _produtoRepository.InserirProduto(produto);
                return Ok();
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.NotAcceptable,_notification.GetNotification());
            }
        }

        public IHttpActionResult Get()
        {
            return Ok(_produtoRepository.ListarProdutos());
        }

        [HttpGet]
        [Route("api/produto/selecionar/{idProduto}")]
        public IHttpActionResult GetId(int idProduto)
        {
            return Ok(_produtoRepository.SelecionarDadosProduto(idProduto));
        }

        public IHttpActionResult Put(ProdutoDto produto)
        {
            _produtoRepository.UpdateProduto(produto);
            return Ok();
        }

        public IHttpActionResult Delete(int idproduto)
        {
            //_produtoRepository.DeletarProduto(idproduto);
            return Ok();
        }
    }
}