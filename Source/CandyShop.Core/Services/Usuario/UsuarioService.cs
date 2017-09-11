using CandyShop.Core.Infra;
using CandyShop.Core.Usuario.Dto;

namespace CandyShop.Core.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Notification _notification;
        private readonly IUsuarioRepository _usuarioRepository;


        public UsuarioService(Notification notification, IUsuarioRepository usuarioRepository)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
        }

        public void InserirUsuario(UsuarioDto usuario)
        {
            if (_notification.HasNotification())
                return;
            _usuarioRepository.InserirUsuario(usuario);
        }
    }
}