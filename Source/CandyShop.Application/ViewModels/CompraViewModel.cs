using System;
using System.Collections.Generic;

namespace CandyShop.Application.ViewModels
{
    public class CompraViewModel
    {
        public int IdCompra { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal? ValorCompra { get; set; }
        public IEnumerable<CompraProdutoViewModel> Itens { get; set; }
    }
}
