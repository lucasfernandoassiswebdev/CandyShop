using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Net.Http;

namespace CandyShop.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Response<IEnumerable<UsuarioViewModel>> ListarUsuarios();
        Response<IEnumerable<UsuarioViewModel>> ListarUsuariosEmDivida();
        Response<string> InserirUsuario(UsuarioViewModel usuario);
        Response<string> EditarUsuario(UsuarioViewModel usuario);
        Response<UsuarioViewModel> SelecionarUsuario(string cpf);
        Response<string> DesativarUsuario(string cpf);
        Response<IEnumerable<UsuarioViewModel>> ListarInativos();
        Response<IEnumerable<UsuarioViewModel>> ProcurarUsuario(string nome);
        HttpResponseMessage VerificaLogin(UsuarioViewModel usuario);
    }
}
