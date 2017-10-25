using CandyShop.Core.Services;
using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.CompraProduto;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    [Authorize]
    public class CompraController : ApiController
    {
        // Interfaces que serão instânciadas pelo simple injector
        private readonly ICompraRepository _compraRepository;
        private readonly ICompraProdutoRepository _compraProdutoRepository;
        private readonly INotification _notification;
        private readonly CompraService _appService;

        /* No construtor da classe, através de injeção de dependência
           as classes serão instânciadas de acordo com as interfaces
           de que herdam, qual classe será instanciada de acordo com a instância
           que é pedida está definido no container do simple injector */
        public CompraController(ICompraRepository compraRepository, ICompraProdutoRepository compraProdutoRepository, INotification notification, CompraService service)
        {
            _compraRepository = compraRepository;
            _compraProdutoRepository = compraProdutoRepository;
            _notification = notification;
            _appService = service;
        }

        /* Método do verbo HTTP POST para inserir uma compra, se algo der errado 
           é adicionada uma notification que voltará como um toast do materialize 
           para o usuário posteriormente, caso contrário retorna Ok(código 200) */
        [HttpPost]
        public IHttpActionResult PostCompra(Compra compra)
        {
           _appService.InserirCompra(compra);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            return Content(HttpStatusCode.OK,"Sua compra foi registrada com sucesso");
        }

        /* Verbos http GET servem para operações no banco que retornam algo (SELECT) ,
           note que nesses verbos o resultado é retornado dentro do método Ok()*/
        [HttpGet]
        public IHttpActionResult GetCompra()
        {
            return Ok(_compraRepository.ListarCompra());
        }

        // Definindo rotas na API
        [HttpGet, Route("api/compra/selecionarcompra/{idCompra}")]
        public IHttpActionResult GetCompraPorId(int idCompra)
        {
            var compra = _compraRepository.SelecionarDadosCompra(idCompra);
            compra.Itens = _compraProdutoRepository.ListarCompraProdutoIdVenda(idCompra);
            return Ok(compra);
        }

        [HttpGet, Route("api/compra/listaCompracpf/{cpf}")]
        public IHttpActionResult GetCpf(string cpf)
        {
            return Ok(_compraRepository.ListarCompraPorCpf(cpf));
        }

        [HttpGet, Route("api/compra/semana")]
        public IHttpActionResult GetSemana()
        {
            return Ok(_compraRepository.ListarCompraSemana());
        }

        [HttpGet, Route("api/compra/mes/{mes}")]
        public IHttpActionResult GetMes(int mes)
        {
            return Ok(_compraRepository.ListarCompraMes(mes));
        }

        [HttpGet, Route("api/compra/dia")]
        public IHttpActionResult GetDia()
        {
            return Ok(_compraRepository.ListarCompraDia());
        }

        [HttpGet, Route("api/compra/{nomeUsuario}")]
        public IHttpActionResult GetNome(string nomeUsuario)
        {
            return Ok(_compraRepository.ListarCompraPorNome(nomeUsuario));
        }
    }
}