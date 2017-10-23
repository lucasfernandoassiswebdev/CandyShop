using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Response<IEnumerable<UsuarioViewModel>> ListarUsuarios(string token);
        Response<IEnumerable<UsuarioViewModel>> ListarUsuariosEmDivida();
        Response<string> InserirUsuario(UsuarioViewModel usuario);
        Response<string> EditarUsuario(UsuarioViewModel usuario);
        Response<string> TrocarSenha(UsuarioViewModel usuario);
        Response<UsuarioViewModel> SelecionarUsuario(string cpf);
        Response<string> DesativarUsuario(UsuarioViewModel usuario);
        Response<IEnumerable<UsuarioViewModel>> ListarInativos();
        Response<IEnumerable<UsuarioViewModel>> ProcurarUsuario(string nome);
        Response<decimal> VerificaCreditoLoja();
        Response<string> VerificaLogin(UsuarioViewModel usuario);
    }
}
