using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Response<IEnumerable<Usuario>> ListarUsuarios();
        Response<IEnumerable<Usuario>> ListarUsuariosEmDivida();
        Response<string> InserirUsuario(Usuario usuario);
    }
}
