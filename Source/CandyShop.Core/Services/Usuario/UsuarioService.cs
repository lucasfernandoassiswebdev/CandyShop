using CandyShop.Core.Services._Interfaces;
using CandyShop.Core.Usuario.Dto;

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
            if (_usuarioRepository.SelecionarUsuario(usuario.Cpf))
            {
                _notification.Add("Usuario ja cadastrado!!!");
                return;
            } 
            _usuarioRepository.InserirUsuario(usuario);
        }
    }
}