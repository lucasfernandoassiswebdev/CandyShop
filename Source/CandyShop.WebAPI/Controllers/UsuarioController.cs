using CandyShop.Core.Services;
using CandyShop.Core.Services.Usuario;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly string _enderecoImagens = $"{ImagensConfig.EnderecoImagens}\\Usuarios";
        private readonly string _getEnderecoImagens = $"{ImagensConfig.GetEnderecoImagens}/Usuarios";
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

            try
            {
                if (usuario.Imagem != null)
                {

                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    if (usuario.Imagem == null) return Ok();
                    foreach (var prefixo in prefixos)
                        if (usuario.Imagem.StartsWith(prefixo))
                        {
                            usuario.Imagem = usuario.Imagem.Substring(prefixo.Length);

                            //transformando base64 em array de bytes
                            var bytes = Convert.FromBase64String(usuario.Imagem);

                            //montando o nome e caminho de save da imagem
                            usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                            var caminho = $"{_enderecoImagens}\\{usuario.Cpf}.jpg";

                            File.WriteAllBytes(caminho, bytes);
                        }
                }else InserirPadrao(usuario.Cpf);
            }
            catch
            {
                return Content(HttpStatusCode.NotModified, "Usuario inserido com sucesso, porém houve um erro ao inserir sua imagem");
            }
            return Ok();
        }

        public IHttpActionResult Put(Usuario usuario)
        {
            usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            _usuarioService.EditarUsuario(usuario);

            if (_notification.HasNotification())
                return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

            if (usuario.Imagem == null) return Content(HttpStatusCode.OK, "Usuário cadastrado com sucesso");
            try
            {
                if (usuario.Imagem != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                        if (usuario.Imagem.StartsWith(prefixo))
                        {
                            usuario.Imagem = usuario.Imagem.Substring(prefixo.Length);

                            //transformando base64 em array de bytes
                            var bytes = Convert.FromBase64String(usuario.Imagem);

                            //montando o nome e caminho de save da imagem
                            usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                            var caminho = $"{_enderecoImagens}\\{usuario.Cpf}.jpg";

                            File.WriteAllBytes(caminho, bytes);
                        }
                }
                else InserirPadrao(usuario.Cpf);

                if (usuario.RemoverImagem)
                {
                    var filePath = $"{_enderecoImagens}\\{usuario.Cpf}.jpg";
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        InserirPadrao(usuario.Cpf);
                    }
                }
            }
            catch
            {
                return Content(HttpStatusCode.NotModified, "Usuario editado com sucesso, porém houve um erro ao editar sua imagem");
            }

            return Content(HttpStatusCode.OK, "Usuário cadastrado com sucesso");
        }

        [HttpPost, Route("api/Usuario/login")]
        public IHttpActionResult PostLogin(Usuario usuario)
        {
            usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            var user = _usuarioRepository.SelecionarUsuario(usuario.Cpf);
            if (user == null || user.Ativo == "I")
                return Content(HttpStatusCode.BadRequest, "O usuário não existe ou foi desativado");

            return _usuarioService.VerificaLogin(usuario) != 0 ? Content(HttpStatusCode.OK, "Logado com sucesso") : Content(HttpStatusCode.BadRequest, "Login ou senha incorretos");
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
            var usuario = _usuarioRepository.SelecionarUsuario(cpf);
            usuario.Imagem = _getEnderecoImagens + "/" + cpf + ".jpg";
            return Ok(usuario);
        }

        private void InserirPadrao(string nome)
        {
            //pegando a imagem na aplicação e transformando em base 64
            var imagem = ConvertTo64();
            //transformando em array de bytes e salvando com o cpf do usuário
            var bytes = Convert.FromBase64String(imagem);
            var caminho = $"{_enderecoImagens}/{nome}.jpg";
            File.WriteAllBytes(caminho, bytes);
        }

        private string ConvertTo64()
        {
            using (var image = Image.FromFile($"{_enderecoImagens}/sem-foto.png"))
            {
                using (var m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    var imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    var base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}