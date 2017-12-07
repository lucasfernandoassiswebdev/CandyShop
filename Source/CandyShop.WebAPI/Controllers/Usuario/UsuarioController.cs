using CandyShop.Core.Services;
using CandyShop.Core.Services.Usuario;
using System;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers.Usuario
{   // Esse atributo diz a classe que suas actions exigem algum tipo de autenticação
    [Authorize]
    public class UsuarioController : UsuarioUnauthorizedController
    {
        private readonly string _enderecoImagens = $"{ImagensConfig.EnderecoImagens}\\Usuarios";
        private readonly string _getEnderecoImagens = $"{ImagensConfig.GetEnderecoImagens}/Usuarios";

        private readonly INotification _notification;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly Imagens _imagens;
        public UsuarioController(INotification notification, IUsuarioRepository usuarioRepository, IUsuarioService usuarioService, Imagens imagens) : base(usuarioService, usuarioRepository, notification)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _imagens = imagens;
        }

        [HttpPost, Route("api/Usuario")]
        public IHttpActionResult Post(Core.Services.Usuario.Usuario usuario)
        {
            if (usuario.Cpf == null)
                return Content(HttpStatusCode.BadRequest, "Os campos de CPF e Nome são obrigatórios para o cadastro!");

            usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            _usuarioService.InserirUsuario(usuario);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            var caminho = $"{_enderecoImagens}\\{usuario.Cpf}";
            try
            {
                if (usuario.Imagem != null)
                    _imagens.InserirImagem(usuario.Imagem, caminho);
                else _imagens.InserirPadrao(caminho);
            }
            catch
            {
                return Content(HttpStatusCode.NotAcceptable, "Usuario inserido, porém houve um erro ao inserir sua imagem");
            }

            return Content(HttpStatusCode.OK, "Usuario inserido com sucesso");
        }

        [HttpPut, Route("api/Usuario/CadastraEmail")]
        public IHttpActionResult PutCadastraEmail(Core.Services.Usuario.Usuario usuario)
        {
            usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty);
            _usuarioService.CadastraEmail(usuario.Email, usuario.Cpf);
            if (_notification.GetNotification() == "OK")
            {
                return Content(HttpStatusCode.OK, _notification.GetNotification());
            }
            return Content(HttpStatusCode.NotAcceptable, _notification.GetNotification());
        }

        [HttpPut, Route("api/Usuario")]
        public IHttpActionResult Put(Core.Services.Usuario.Usuario usuario)
        {
            if (usuario.Cpf == null)
                return Content(HttpStatusCode.BadRequest, "Os campos de CPF e Nome são obrigatórios para o cadastro!");

            usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            _usuarioService.EditarUsuario(usuario);

            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            var caminho = $"{_enderecoImagens}\\{usuario.Cpf}";
            try
            {
                if (usuario.Imagem != null)
                    _imagens.InserirImagem(usuario.Imagem, caminho);
                if (usuario.RemoverImagem)
                    _imagens.RemoverImagem(caminho);
            }
            catch
            {
                return Content(HttpStatusCode.NotModified, "Usuario editado, porém houve um erro ao editar sua imagem");
            }

            return Content(HttpStatusCode.OK, "Usuário cadastrado com sucesso");
        }
        [HttpPut, Route("api/Usuario/trocarSenha")]
        public IHttpActionResult PutSenha(Core.Services.Usuario.Usuario usuario)
        {
            _usuarioService.VerificaSenha(usuario.SenhaUsuario);
            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
            _usuarioRepository.TrocarSenha(usuario);
            return Ok();
        }
        [HttpPut, Route("api/Usuario/desativar/{cpf}")]
        public IHttpActionResult PutDesativar(Core.Services.Usuario.Usuario usuario)
        {
            return _usuarioRepository.DesativarUsuario(usuario.Cpf) == 1 ? Content(HttpStatusCode.NotAcceptable, "Quantidade de minima de administradores atingida") : Content(HttpStatusCode.OK, "Usuario desativado com sucesso");
        }

        /* Quando mais de um método com o mesmo verbo HTTP(no caso o GET) é necessário, 
            são definidas rotas como no exemplo abaixo, essas rotas determinarão qual dos 
            métodos da API será chamado */
        [Route("api/Usuario")]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, _usuarioRepository.ListarUsuario());
        }
        [HttpGet, Route("api/Usuario/Devedores")]
        public IHttpActionResult GetUsuariosDivida()
        {
            return Ok(_usuarioRepository.ListarUsuarioDivida());
        }
        [HttpGet, Route("api/Usuario/inativos")]
        public IHttpActionResult GetUsuariosInativos()
        {
            return Ok(_usuarioRepository.ListarUsuariosInativos());
        }
        [HttpGet, Route("api/Usuario/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            return Ok(_usuarioRepository.ListarUsuarioPorNome(nome));
        }
        [HttpGet, Route("api/Usuario/saldo")]
        public IHttpActionResult GetSaldo()
        {
            return Ok(_usuarioRepository.VerificaCreditoLoja());
        }

        [HttpGet, Route("api/Usuario/{cpf}/Detalhes")]
        public IHttpActionResult GetUsuario(string cpf)
        {
            var usuario = _usuarioRepository.SelecionarUsuario(cpf);
            usuario.Imagem = $"{_getEnderecoImagens}/{cpf}.jpg?={DateTime.Now.Ticks}";
            return Ok(usuario);
        }
    }
}