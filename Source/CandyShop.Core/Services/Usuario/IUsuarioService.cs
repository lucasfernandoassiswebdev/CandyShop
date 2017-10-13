namespace CandyShop.Core.Services.Usuario
{
    public interface IUsuarioService
    {
        void InserirUsuario(Usuario usuario);
        void EditarUsuario(Usuario usuario);
        string VerificaLogin(Usuario usuario);
    }
}