using CandyShop.Core;
using CandyShop.Core.Infra;
using CandyShop.Core.Services.Usuario;
using CandyShop.Core.Usuario.Dto;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly Notification _notification;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;


        public UsuarioController(Notification notification, IUsuarioRepository usuarioRepository,
            IUsuarioService usuarioService)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }

        public IHttpActionResult InserirUsuario(UsuarioDto usuario)
        {
            _usuarioService.InserirUsuario(usuario);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.NotAcceptable, _notification.GetNotification());
            return Ok();
        }

        public IHttpActionResult ListarUsuario(string cpf)
        {
            _usuarioRepository.ListarUsuario();
            return Ok();
        }

        public IHttpActionResult EditaUsuario(UsuarioDto usuario)
        {
            _usuarioRepository.EditarUsuario(usuario);
            return Ok();
        }

        public IHttpActionResult DeletaUsuario(string cpf)
        {
            _usuarioRepository.DeletarUsuario(cpf);
            return Ok();
        }

    }
}