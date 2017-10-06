using CandyShop.Core.Services;
using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Produto.Dto;
using System;
using System.Linq;
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
                _produtoService.IsValid(produto);
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

                int sequencial;
                var result = _produtoRepository.InserirProduto(produto, out sequencial);
                if (result == -1)
                    return Content(HttpStatusCode.BadRequest, "[Falha ao inserir o produto]");
                return Ok(sequencial);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
            
        }        

        public IHttpActionResult Put(ProdutoDto produto)
        {
            try
            {
                _produtoRepository.UpdateProduto(produto);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpPut]
        [Route("api/produto/desativar/{idProduto}")]
        public IHttpActionResult PutDesativar(int idproduto)
        {
            try
            {
                _produtoRepository.DesativarProduto(idproduto);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        #region GETS

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_produtoRepository.ListarProdutos());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/produto/inativos")]
        public IHttpActionResult GetInativos()
        {
            try
            {
                return Ok(_produtoRepository.ListarProdutosInativos());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }
        
        [HttpGet, Route("api/produto/selecionar/{idProduto}")]
        public IHttpActionResult GetId(int idProduto)
        {
            try
            {
                return Ok(_produtoRepository.SelecionarDadosProduto(idProduto));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }
        
        [HttpGet, Route("api/produto/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            try
            {
                return Ok(_produtoRepository.ProcurarProdutoPorNome(nome));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/produto/categoria/{categoria}")]
        public IHttpActionResult GetCategoria(string categoria)
        {
            try
            {
                return Ok(_produtoRepository.ListarProdutosPorCategoria(categoria));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        #endregion
    }
}