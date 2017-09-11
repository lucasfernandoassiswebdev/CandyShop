using CandyShop.Core.Usuario.Dto;
using System;

namespace CandyShop.Core.Compra.Dto
{
    public class CompraDto
    {
        public int IdCompra { get; set; }
        public UsuarioDto Usuario { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
