using CandyShop.Core.Services;
using CandyShop.Core.Services.Usuario;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly INotification _notification;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(INotification notification, IUsuarioRepository usuarioRepository, IUsuarioService usuarioService)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }

        public IHttpActionResult Post(Usuario usuario)
        {
            _usuarioService.InserirUsuario(usuario);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            return Ok();
        }

        [HttpPost, Route("api/Usuario/login")]
        public IHttpActionResult PostLogin(Usuario usuario)
        {
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            var user = _usuarioRepository.SelecionarUsuario(usuario.Cpf);

            if (user == null || user.Ativo == "I")
                return Content(HttpStatusCode.BadRequest, "O usuário não existe ou foi desativado");

            if (user.Ativo != "I")
                return Ok(_usuarioService.VerificaLogin(usuario));

            return BadRequest();
        }

        public IHttpActionResult Get()
        {
            return Ok(_usuarioRepository.ListarUsuario());
        }

        /* Quando mais de um método com o mesmo verbo HTTP(no caso o GET) é necessário, 
           são definidas rotas como no exemplo abaixo, essas rotas determinarão qual dos 
           métodos da API será chamado */
        [HttpGet, Route("api/Usuario/Devedores")] 
        public IHttpActionResult GetUsuariosDivida()
        {
            return Ok(_usuarioRepository.ListarUsuarioDivida());
        }

        [HttpGet, Route("api/Usuario/inativos")] 
        public IHttpActionResult GetUsuariosInativos()
        {
            return Ok(_usuarioRepository.ListarUsuarioInativo());
        }

        [HttpGet, Route("api/usuario/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            return Ok(_usuarioRepository.ListarUsuarioPorNome(nome));
        }

        [HttpGet, Route("api/Usuario/saldo")]
        public IHttpActionResult GetSaldo()
        {
            return Ok(_usuarioRepository.VerificaCreditoLoja());
        }

        public IHttpActionResult Put(Usuario usuario)
        {
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            _usuarioRepository.EditarUsuario(usuario);
            return Ok();
        }

        [HttpPut, Route("api/usuario/trocarSenha")]
        public IHttpActionResult PutSenha(Usuario usuario)
        {
            _usuarioRepository.TrocarSenha(usuario);
            return Ok();
        }

        [HttpPut, Route("api/usuario/desativar/{cpf}")]
        public IHttpActionResult PutDesativar(Usuario usuario)
        {
            _usuarioRepository.DesativarUsuario(usuario.Cpf);
            return Ok();
        }

        [HttpGet, Route("api/Usuario/{cpf}/Detalhes")]
        public IHttpActionResult GetWithCpf(string cpf)
        {
            return Ok(_usuarioRepository.SelecionarUsuario(cpf));
        }
    }
}