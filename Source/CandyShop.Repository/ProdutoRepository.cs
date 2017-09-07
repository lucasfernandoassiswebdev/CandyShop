
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
            AddParameter("@NomeProduto", produto.NomeProduto);
            AddParameter("@PrecoProduto", produto.PrecoProduto);
            AddParameter("@QtdeProduto", produto.QtdeProduto);

            ExecuteNonQuery();
        }

        public void DeletaProduto(int idproduto)
        {
            ExecuteProcedure(Procedures.GCS_DelProduto);
            AddParameter("@IdProduto", idproduto);
            ExecuteNonQuery();
        }

        public void UpdateProduto(ProdutoDto produto)
        {
            ExecuteProcedure(Procedures.GCS_UpdProduto);
            AddParameter("@IdProduto", produto.IdProduto);
            AddParameter("@NomeProduto", produto.NomeProduto);
            AddParameter("@PrecoProduto", produto.PrecoProduto);
            AddParameter("@QtdeProduto", produto.QtdeProduto);
            ExecuteNonQuery();
        }

        public bool SelecionarProdutos(int idproduto)
        {
            ExecuteProcedure(Procedures.GCS_SelProduto);
            AddParameter("@IdProduto", idproduto);
            using (var retornobd = ExecuteReader())
                return retornobd.Read();
        }
    }
}