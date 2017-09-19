using CandyShop.Core.Services.Usuario.Dto;

namespace CandyShop.Core.Services.Usuario
{
    public interface IUsuarioService
    {
        void InserirUsuario(UsuarioDto usuario);
        void EditarUsuario(UsuarioDto usuario);
    }
}