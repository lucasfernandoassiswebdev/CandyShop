using System;

namespace CandyShop.Core.Pagamento.Dto
{
    public class PagamentoDto
    {
        public int IdPagamento { get; set; }
        public int Usuario { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }

    }
}
