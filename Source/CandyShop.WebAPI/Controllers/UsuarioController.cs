using CandyShop.Core.Services;
using CandyShop.Core.Services.Usuario;
using CandyShop.Core.Services.Usuario.Dto;
using System;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly INotification _notification;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(INotification notification, IUsuarioRepository usuarioRepository,IUsuarioService usuarioService)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }

        public IHttpActionResult Post(UsuarioDto usuario)
        {
            try
            {
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
                _usuarioRepository.InserirUsuario(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        public IHttpActionResult Get()
        {
            return Ok(_usuarioRepository.ListarUsuario());
        }

        [HttpGet, Route("api/Usuario/Devedores")] // Colocar quando controller tiver mais de um metodos GET
        public IHttpActionResult GetUsuariosDivida()
        {
            return Ok(_usuarioRepository.ListarUsuarioDivida());
        }

        [HttpGet, Route("api/Usuario/inativos")] // Colocar quando controller tiver mais de um metodos GET
        public IHttpActionResult GetUsuariosInativos()
        {
            return Ok(_usuarioRepository.ListarUsuarioInativo());
        }        

        public IHttpActionResult Put(UsuarioDto usuario)
        {
            try
            {
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
                _usuarioRepository.EditarUsuario(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/usuario/desativar/{cpf}")]
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