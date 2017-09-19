using CandyShop.Core.Services.CompraProduto.Dto;
using CandyShop.Core.Services.Usuario.Dto;
using System;
using System.Collections.Generic;

namespace CandyShop.Core.Services.Compra.Dto
{
    public class CompraDto
    {
        public int IdCompra { get; set; }
        public UsuarioDto Usuario { get; set; }
        public DateTime DataCompra { get; set; }
        public IEnumerable<CompraProdutoDto> Itens { get; set; }
    }
}
