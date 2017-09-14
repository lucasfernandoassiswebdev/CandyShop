using System;

namespace CandyShop.Application.ViewModels
{
    public class Pagamento
    {
        public int IdPagamento { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }

    }
}
