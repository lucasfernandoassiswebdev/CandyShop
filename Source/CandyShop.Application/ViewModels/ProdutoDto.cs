﻿namespace CandyShop.Application.ViewModels
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int QtdeProduto { get; set; }
        public string Ativo { get; set; }
        public string Categoria { get; set; }
    }
}