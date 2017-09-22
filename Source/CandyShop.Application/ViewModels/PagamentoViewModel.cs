using System;

namespace CandyShop.Application.ViewModels
{
    public class PagamentoViewModel
    {
        public int IdPagamento { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
    }
}
