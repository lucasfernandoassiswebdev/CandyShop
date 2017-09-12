using CandyShop.Core.CompraProduto.Dto;
using CandyShop.Core.Usuario.Dto;
using System;
using System.Collections.Generic;

namespace CandyShop.Core.Compra.Dto
{
    public class CompraDto
    {
        public int IdCompra { get; set; }
        public UsuarioDto Usuario { get; set; }
        public DateTime DataCompra { get; set; }
        public IEnumerable<CompraProdutoDto> Itens { get; set; }
    }
}
