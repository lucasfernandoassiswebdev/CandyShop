﻿using System.Collections.Generic;

namespace CandyShop.Core.Services.Usuario
{
    public interface IUsuarioRepository
    {
        void InserirUsuario(Usuario usuario);
        void EditarUsuario(Usuario usuario);
        int DesativarUsuario(string cpf);
        void TrocarSenha(Usuario usuario);
        int VericaUsuarioIgual(Usuario usuario);
        Usuario SelecionarUsuario(string cpf);
        IEnumerable<Usuario> ListarUsuario();
        IEnumerable<Usuario> ListarUsuarioInativo();
        IEnumerable<Usuario> ListarUsuarioDivida();
        IEnumerable<Usuario> ListarUsuarioPorNome(string nome);
        int VerificaLogin(Usuario usuario);
        decimal VerificaCreditoLoja();
    }
}
