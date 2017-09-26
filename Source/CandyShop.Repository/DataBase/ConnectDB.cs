using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CandyShop.Repository.Database
{
    public class ConnectDB 
    {
        //Cria o construtor pra que toda vez que o _connection for instanciado abrir a conexão com o banco 
        public ConnectDB()
        {
            _connection = Connect();
        }
        // Pega a minha connection string que esta no webConfig
        private string _connectionString => ConfigurationManager.ConnectionStrings["DbCandyShop"].ToString();
        private readonly SqlConnection _connection;
        private SqlCommand _command;
        /* Testa a conexão com o banco se ela estiver quebrada entao fecha e abre denovo e se a conexao
            estiver fechada abre ela*/
        private SqlConnection Connect()
        {
            var connection = new SqlConnection(_connectionString);

            if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
        // Cria o metodo para executar procedure 
        public void ExecuteProcedure(object procName)
        {
            _command = new SqlCommand(procName.ToString(), _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }
        public void AddParameter(string parameterName, object parameterValue)
        {
            _command.Parameters.AddWithValue(parameterName, parameterValue);
        }
        // Método para executar procedure que não tem nenhum retorno (Insert,Delete)
        public void ExecuteNonQuery()
        {
            _command.ExecuteNonQuery();
        }
        // Método para executar procedure que tem nenhum retorno (Insert,Delete)
        public int ExecuteNonQueryWithReturn()
        {
            _command.ExecuteNonQuery();
            return int.Parse(_command.Parameters["@RETURN_VALUE"].Value.ToString());
        }
        // Metodo exclusivo para procedure que retorna valores (Select)
        public SqlDataReader ExecuteReader()
        {
            return _command.ExecuteReader();
        }
    }
}