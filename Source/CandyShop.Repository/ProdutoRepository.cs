
using CandyShop.Core.Services.Produto.Dto;

namespace CandyShop.Repository


{
    public class ProdutoRepository : ConnectDB 
    {
        private enum Procedures
        {
            GCS_InsProduto,
            GCS_DelProduto,
            GCS_UpdProduto,
            GCS_SelProduto
        }

        public void InserirProduto(ProdutoDto produto)
        {
            ExecuteProcedure(Procedures.GCS_InsProduto);
            AddParameter("NomeProduto",produto.NomeProduto);
            AddParameter("PrecoProduto",produto.PrecoProduto);
            AddParameter("QtdeProduto",produto.QtdeProduto);
        }
    }
}