namespace CandyShop.Core.Services.Usuario
{
    public interface IUsuarioService
    {
        void InserirUsuario(Usuario usuario);
        void EditarUsuario(Usuario usuario);
        int VerificaLogin(Usuario usuario);
        void VerificaSenha(string novaSenha);
    }
}