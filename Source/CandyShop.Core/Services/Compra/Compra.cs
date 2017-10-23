using System;
using System.Collections.Generic;

namespace CandyShop.Core.Services.Compra
{
    /* Essa classe é uma entidade, uma representação de um objeto
       do mundo real, nesse caso uma compra, com Id para identificar
       a compra, usuário que a realizou, data, valor e os itens que
       foram comprados no ato. */
    public class Compra
    {
        public int IdCompra { get; set; }
        public Usuario.Usuario Usuario { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal? ValorCompra { get; set; }
        public IEnumerable<CompraProduto.CompraProduto> Itens { get; set; }
    }
}
