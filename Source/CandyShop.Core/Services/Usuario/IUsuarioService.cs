namespace CandyShop.Core.Services.Usuario
{
    public interface IUsuarioService
    {
        void InserirUsuario(Usuario usuario);
        void EditarUsuario(Usuario usuario);
        int VerificaLogin(Usuario usuario);
        void AlteraSenha(string cpf);
        void CadastraEmail(string novoEmail, string cpf);
        void VerificaSenha(string novaSenha);
    }
}