namespace CandyShop.Application.ViewModels
{

    public class UsuarioViewModel
    {
        public string Cpf { get; set; }
        public string NomeUsuario { get; set; } 
        public string SenhaUsuario { get; set; }
        public decimal SaldoUsuario { get; set; }
        public string Ativo { get; set; }
        public string Imagem { get; set; }
        public bool RemoverImagem { get; set; }
        public string Classificacao { get; set; }
        public string FirstLogin { get; set; }
        public string Email { get; set; }
    }
}
