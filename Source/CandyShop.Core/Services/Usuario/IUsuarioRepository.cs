using CandyShop.Core.Usuario.Dto;
using System.Collections.Generic;

namespace CandyShop.Core
{
    public interface IUsuarioRepository
    {
        void InserirUsuario(UsuarioDto usuario);
        void EditarUsuario(UsuarioDto usuario);
        void DeletarUsuario(string cpf);
        bool SelecionarUsuario(string cpf);
        IEnumerable<UsuarioDto> ListarUsuario();
        IEnumerable<UsuarioDto> ListarUsuarioDivida();
        UsuarioDto SelecionarDadosUsuario(string cpf);
    }
}
