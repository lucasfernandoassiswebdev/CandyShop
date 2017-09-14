
using System;
using System.Collections.Generic;

namespace CandyShop.Application.ViewModels
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataCompra { get; set; }
        public IEnumerable<CompraProduto> Itens { get; set; }
    }
}
