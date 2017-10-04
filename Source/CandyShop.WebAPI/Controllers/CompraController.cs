using CandyShop.Core.Services;
using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.Compra.Dto;
using CandyShop.Core.Services.CompraProduto;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class CompraController : ApiController
    {
        private readonly ICompraRepository _compraRepository;
        private readonly ICompraProdutoRepository _compraProdutoRepository;
        private readonly INotification _notification;
        private readonly CompraService _appService;
        

        public CompraController(ICompraRepository compraRepository, ICompraProdutoRepository compraProdutoRepository, INotification notification, CompraService service)
        {
            _compraRepository = compraRepository;            
            _compraProdutoRepository = compraProdutoRepository;
            _notification = notification;
            _appService = service;
        }

        //Método post para inserir uma compra, se der erro adiciona uma notification senão retorna Ok(200)
        [HttpPost]
        public IHttpActionResult PostCompra(CompraDto compra)
        {
            try
            {
                var result = _appService.InserirCompra(compra);
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
                return Ok(result);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpPut]
        public IHttpActionResult PutCompra(CompraDto compra)
        {
            try
            {
                _compraRepository.EditarCompra(compra);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        #region Gets
        [HttpGet]
        public IHttpActionResult GetCompra()
        {
            try
            {
                return Ok(_compraRepository.ListarCompra());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/compra/selecionarcompra/{idCompra}")]
        public IHttpActionResult GetUmaCompra(int idCompra)
        {
            try
            {
                var compra = _compraRepository.SelecionarDadosCompra(idCompra);
                compra.Itens = _compraProdutoRepository.ListarCompraProdutoIdVenda(idCompra);
                return Ok(compra);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/compra/listaCompracpf/{cpf}")]
        public IHttpActionResult GetCpf(string cpf)
        {
            try
            {
                return Ok(_compraRepository.ListarCompraPorCpf(cpf));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/compra/semana")]
        public IHttpActionResult GetSemana()
        {
            try
            {
                return Ok(_compraRepository.ListarCompraSemana());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/compra/mes/{mes}")]
        public IHttpActionResult GetMes(int mes)
        {
            try
            {
                return Ok(_compraRepository.ListarCompraMes(mes));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }
        [HttpGet, Route("api/compra/dia")]
        public IHttpActionResult GetDia()
        {
            try
            {
                return Ok(_compraRepository.ListarCompraDia());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }

        [HttpGet, Route("api/compra/{nomeUsuario}")]
        public IHttpActionResult GetNome(string nomeUsuario)
        {
            try
            {
                return Ok(_compraRepository.ListarCompraPorNome(nomeUsuario));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message.ToList());
            }
        }
        #endregion
        
    }
}