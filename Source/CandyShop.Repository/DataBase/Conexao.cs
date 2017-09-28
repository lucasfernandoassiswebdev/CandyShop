using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CandyShop.Repository.DataBase
{
    public class Conexao
    {
        public readonly SqlConnection Connection;
        public SqlTransaction Transaction;

        public Conexao()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCandyShop"].ToString());
        }

        public SqlConnection Open()
        {
            if (Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
                Connection.Open();
            }

            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            return Connection;
        }

        public void BeginTransaction()
        {
            Open();
            Transaction = Connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Transaction.Commit();
        }

        public void RollBackTransaction()
        {
            Transaction.Rollback();
        }
    }
}
