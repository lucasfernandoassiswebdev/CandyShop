using CandyShop.Core.Services.Usuario;
using System;
using System.Net;
using System.Web.Http;
using CandyShop.Core.Services;

namespace CandyShop.WebAPI.Controllers.Usuario
{
    public class UsuarioUnauthorizedController : ApiController
    {
        private readonly INotification _notification;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly string _getEnderecoImagens = $"{ImagensConfig.GetEnderecoImagens}/Usuarios";

        public UsuarioUnauthorizedController(IUsuarioService usuarioService, IUsuarioRepository repository, INotification notification)
        {
            _usuarioService = usuarioService;
            _usuarioRepository = repository;
            _notification = notification;
        }

        [HttpPost, Route("api/UsuarioUnauthorized/login")]
        public IHttpActionResult PostLogin(Core.Services.Usuario.Usuario usuario)
        {
            usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            var user = _usuarioRepository.SelecionarUsuario(usuario.Cpf);
            if (user == null || user.Ativo == "I")
                return Content(HttpStatusCode.BadRequest, "O usuário não existe ou foi desativado");

            return _usuarioService.VerificaLogin(usuario) != 0 ? Content(HttpStatusCode.OK, "Logado com sucesso") : Content(HttpStatusCode.BadRequest, "Login ou senha incorretos");
        }

        [HttpGet, Route("api/UsuarioUnauthorized/{cpf}/Detalhes")]
        public IHttpActionResult GetWithCpf(string cpf)
        {
            var usuario = _usuarioRepository.SelecionarUsuario(cpf);
            usuario.Imagem = $"{_getEnderecoImagens}/{cpf}.jpg?={DateTime.Now.Ticks}";
            return Ok(usuario);
        }

        [HttpGet, Route("api/UsuarioUnauthorized/{cpf}/VerificaEmailExiste")]
        public IHttpActionResult GetVerificaEmailExiste(string cpf)
        {
            _usuarioService.AlteraSenha(cpf);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            return Ok();
        }
    }
}