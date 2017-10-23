using System;

namespace CandyShop.Core.Services.Pagamento
{
    public class Pagamento
    {
        public int IdPagamento { get; set; }
        public Usuario.Usuario Usuario { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
    }
}
