using CandyShop.Core.Services._Interfaces;
using CandyShop.Core.Services.Usuario;
using CandyShop.Core.Usuario.Dto;
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

        public IHttpActionResult Post(UsuarioDto usuario)
        {
            _usuarioService.InserirUsuario(usuario);

            if (_notification.HasNotification())
                return Content(HttpStatusCode.NotAcceptable, _notification.GetNotification());

            return Ok();
        }

        public IHttpActionResult Get()
        {
            return Ok(_usuarioRepository.ListarUsuario());
        }

        [HttpGet, Route("api/Usuario/UsuariosDivida")] // Colocar quando controller tiver mais de um metodos GET
        public IHttpActionResult GetUsuariosDivida()
        {
            return Ok(_usuarioRepository.ListarUsuarioDivida());
        }

        public IHttpActionResult Put(UsuarioDto usuario)
        {
            _usuarioService.EditarUsuario(usuario);

            if (_notification.HasNotification())
                return Content(HttpStatusCode.NotAcceptable, _notification.GetNotification());

            return Ok();
        }

        public IHttpActionResult Delete(string cpf)
        {
            _usuarioRepository.DesativarUsuario(cpf);
            return Ok();
        }

        [HttpGet, Route("api/Usuario/{cpf}/Detalhes")]
        public IHttpActionResult GetWithCpf(string cpf)
        {
            return Ok(_usuarioRepository.SelecionarUsuario(cpf));
        }

    }
}