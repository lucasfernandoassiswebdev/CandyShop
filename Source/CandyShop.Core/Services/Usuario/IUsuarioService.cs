using CandyShop.Core.Usuario.Dto;

namespace CandyShop.Core.Services.Usuario
{
    public interface IUsuarioService
    {
        void InserirUsuario(UsuarioDto usuario);
    }
}