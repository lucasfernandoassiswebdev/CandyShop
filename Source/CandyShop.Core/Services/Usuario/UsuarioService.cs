using CandyShop.Core.Services.Usuario.Dto;
using System.Linq;

namespace CandyShop.Core.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly INotification _notification;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(INotification notification, IUsuarioRepository usuarioRepository)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
        }

        public void InserirUsuario(UsuarioDto usuario)
        {
            if (!usuario.IsValid(_notification))
                return;

            //verificando se não está sendo cadastrado um cpf repetido
            var usuarios = _usuarioRepository.ListarUsuario();
            if (usuarios.Any(usuarioA => usuarioA.Cpf == usuario.Cpf))
            {
                _notification.Add("Este Cpf já existe!");
                return;
            }

            _usuarioRepository.InserirUsuario(usuario);
        }

        public void EditarUsuario(UsuarioDto usuario)
        {
            if (!usuario.IsValid(_notification))
                return;

            //verificando se não está sendo cadastrado um cpf repetido
            var usuarios = _usuarioRepository.ListarUsuario();
            if (usuarios.Any(usuarioA => usuarioA.Cpf == usuario.Cpf))
            {
                _notification.Add("Este Cpf já existe!");
                return;
            }

            _usuarioRepository.EditarUsuario(usuario);
        }

        public int VerificaLogin(UsuarioDto usuario)
        {
            var retorno = _usuarioRepository.VerificaLogin(usuario) == 1 ? 1 : 0;
            return retorno;
        }

    }
}