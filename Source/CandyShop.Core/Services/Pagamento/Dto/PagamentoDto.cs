using CandyShop.Core.Usuario.Dto;
using System;

namespace CandyShop.Core.Pagamento.Dto
{
    public class PagamentoDto
    {
        public int IdPagamento { get; set; }
        public UsuarioDto Usuario { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public string NomeUsuario { get; set; }
    }
}
