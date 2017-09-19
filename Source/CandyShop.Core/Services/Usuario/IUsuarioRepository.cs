using CandyShop.Core.Services.Usuario.Dto;
using System.Collections.Generic;

namespace CandyShop.Core.Services.Usuario
{
    public interface IUsuarioRepository
    {
        void InserirUsuario(UsuarioDto usuario);
        void EditarUsuario(UsuarioDto usuario);
        void DesativarUsuario(string cpf);
        int VericaUsuarioIgual(UsuarioDto usuario);
        UsuarioDto SelecionarUsuario(string cpf);
        IEnumerable<UsuarioDto> ListarUsuario();
        IEnumerable<UsuarioDto> ListarUsuarioDivida();
        UsuarioDto SelecionarDadosUsuario(string cpf);
    }
}
