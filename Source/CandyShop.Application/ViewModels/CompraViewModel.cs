using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyShop.Application.ViewModels
{
    public class CompraViewModel
    {
        public int IdCompra { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal? ValorCompra { get; set; }
        public IEnumerable<CompraProdutoViewModel> Itens { get; set; }
        public string NomeItem => Itens != null ? Itens.Count() == 1 ? Itens.First().Produto.NomeProduto : "Mais de um item..." : "Erro ao buscar item";
    }
}
