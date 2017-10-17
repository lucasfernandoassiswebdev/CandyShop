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

        public void InserirUsuario(Usuario usuario)
        {
            if (!usuario.VerificaInsercao(_notification))
                return;

            //verificando se não está sendo cadastrado um cpf repetido
            var usuarios = _usuarioRepository.ListarUsuario();
            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            if (usuarios.Any(usuarioA => usuarioA.Cpf == cpf))
            {
                _notification.Add("Este Cpf já existe!");
                return;
            }

            _usuarioRepository.InserirUsuario(usuario);
        }

        public void EditarUsuario(Usuario usuario)
        {
            if (!usuario.VerificaEdicao(_notification))
                return;
            _usuarioRepository.EditarUsuario(usuario);
        }

        public int VerificaLogin(Usuario usuario)
        {
            return _usuarioRepository.VerificaLogin(usuario) == 1 ? 1 : 0;
        }

    }
}