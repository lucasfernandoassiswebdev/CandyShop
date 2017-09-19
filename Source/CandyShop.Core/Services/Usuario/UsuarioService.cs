using CandyShop.Core.Services.Usuario.Dto;

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
            if (_usuarioRepository.VericaUsuarioIgual(usuario) == 1)
            {
                _notification.Add("Usuario ja cadastrado!!!");
                return;
            } 

            _usuarioRepository.InserirUsuario(usuario);
        }

        public void EditarUsuario(UsuarioDto usuario)
        {
            if (_usuarioRepository.VericaUsuarioIgual(usuario) == 1)
            {
                _notification.Add("Usuario ja cadastrado!!!");
                return; 
            }

            _usuarioRepository.EditarUsuario(usuario);
        }
    }
}