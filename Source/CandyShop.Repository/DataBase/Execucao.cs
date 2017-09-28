using System.Data;
using System.Data.SqlClient;

namespace CandyShop.Repository.DataBase
{
    public class Execucao
    {
        private readonly Conexao _conexao;
        private SqlCommand _command;

        public Execucao(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void ExecuteProcedure(object procName)
        {
            _command = new SqlCommand(procName.ToString(), _conexao.Connection, _conexao.Transaction)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = int.MaxValue
            };
        }

        public void AddParameter(string parameterName, object parameterValue)
        {
            _command.Parameters.AddWithValue(parameterName, parameterValue);
        }

        protected void AddParameterOutput(string parameterName, object parameterValue, DbType parameterType)
        {
            _command.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.Output,
                Value = parameterValue,
                DbType = parameterType
            });
        }

        protected string GetParameterOutput(string parameter) => _command.Parameters[parameter].Value.ToString();

        // Método para executar procedure que não tem nenhum retorno (Insert,Delete)
        public void ExecuteNonQuery()
        {
            _conexao.Open();
            _command.ExecuteNonQuery();
        }

        protected void AddParameterReturn(string parameterName = "@RETURN_VALUE", DbType parameterType = DbType.Int16)
        {
            _command.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.ReturnValue,
                DbType = parameterType
            });
        }

        // Método para executar procedure que tem nenhum retorno (Insert,Delete)
        public int ExecuteNonQueryWithReturn()
        {
            AddParameterReturn();
            _conexao.Open();
            _command.ExecuteNonQuery();
            return int.Parse(_command.Parameters["@RETURN_VALUE"].Value.ToString());
        }

        // Metodo exclusivo para procedure que retorna valores (Select)
        public SqlDataReader ExecuteReader()
        {
            _conexao.Open();
            return _command.ExecuteReader();
        }

        public void BeginTransaction()
        {
            _conexao.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _conexao.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _conexao.RollBackTransaction();
        }
    }
}
