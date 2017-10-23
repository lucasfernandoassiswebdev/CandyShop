using CandyShop.Core.Services.Produto;
using System;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers.Produto
{
    public class ProdutoUnauthorizedController : ApiController
    {
        private readonly string _getEnderecoImagens = $"{ImagensConfig.GetEnderecoImagens}/Produtos";
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoUnauthorizedController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IHttpActionResult Get()
        {
            var produtos = _produtoRepository.ListarProdutos();
            foreach (var produto in produtos)
            {
                produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
                produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
                produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            }
            return Ok(produtos);
        }

        [HttpGet, Route("api/ProdutoUnauthorized/selecionar/{idProduto}")]
        public IHttpActionResult GetId(int idProduto)
        {
            var produto = _produtoRepository.SelecionarDadosProduto(idProduto);

            produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
            produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
            produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            return Ok(produto);
        }

        [HttpGet, Route("api/ProdutoUnauthorized/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            var produtos = _produtoRepository.ProcurarProdutoPorNome(nome);
            foreach (var produto in produtos)
            {
                produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
                produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
                produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            }
            return Ok(produtos);
        }

        [HttpGet, Route("api/ProdutoUnauthorized/categoria/{categoria}")]
        public IHttpActionResult GetCategoria(string categoria)
        {
            var produtos = _produtoRepository.ListarProdutosPorCategoria(categoria);
            foreach (var produto in produtos)
            {
                produto.ImagemA = $"{_getEnderecoImagens}/{produto.IdProduto}_A.jpg?={DateTime.Now.Ticks}";
                produto.ImagemB = $"{_getEnderecoImagens}/{produto.IdProduto}_B.jpg?={DateTime.Now.Ticks}";
                produto.ImagemC = $"{_getEnderecoImagens}/{produto.IdProduto}_C.jpg?={DateTime.Now.Ticks}";
            }
            return Ok(produtos);
        }
    }
}
