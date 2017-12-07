using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Response<IEnumerable<UsuarioViewModel>> ListarUsuarios(string token);
        Response<IEnumerable<UsuarioViewModel>> ListarUsuariosEmDivida(string token);
        Response<string> InserirUsuario(UsuarioViewModel usuario, string token);
        Response<string> EditarUsuario(UsuarioViewModel usuario, string token);
        Response<string> TrocarSenha(UsuarioViewModel usuario, string token);
        Response<UsuarioViewModel> SelecionarUsuario(string cpf);
        Response<UsuarioViewModel> SelecionarUsuario(string cpf, string token);
        Response<string> DesativarUsuario(UsuarioViewModel usuario, string token);
        Response<IEnumerable<UsuarioViewModel>> ListarInativos(string token);
        Response<IEnumerable<UsuarioViewModel>> ProcurarUsuario(string nome, string token);
        Response<decimal> VerificaCreditoLoja(string token);
        Response<string> VerificaLogin(UsuarioViewModel usuario);
        Response<string> VerificaEmailExiste(string cpf);
        Response<string> CadastraEmail(UsuarioViewModel usuario,string token);
    }
}
