using CandyShop.Core.Services.Pagamento;
using CandyShop.Core.Services.Usuario;
using CandyShop.Repository.DataBase;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys
{
    public class PagamentoRepository : Execucao, IPagamentoRepository
    {
        public PagamentoRepository(Conexao conexao) : base(conexao)
        {

        }

        private enum Procedures
        {
            CSSP_InsPagamento,
            CSSP_UpdPagamento,
            CSSP_DelPagamento,
            CSSP_LisPagamento,
            CSSP_LisPagamentoSemana,
            CSSP_SelPagamento,
            CSSP_ListarPagamentoDia
        }

        public void InserirPagamento(Pagamento pagamento)
        {
            ExecuteProcedure(Procedures.CSSP_InsPagamento);
            AddParameter("@Cpf", pagamento.Usuario.Cpf);
            AddParameter("@ValorPagamento", pagamento.ValorPagamento);
            ExecuteNonQuery();
        }
        public void EditarPagamento(Pagamento pagamento)
        {
            ExecuteProcedure(Procedures.CSSP_UpdPagamento);
            AddParameter("@IdPagamento", pagamento.IdPagamento);            
            AddParameter("@ValorPagamento", pagamento.ValorPagamento);

            ExecuteNonQuery();
        }
        public void DeletarPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.CSSP_DelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            ExecuteNonQuery();
        }

        public bool SelecionarPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.CSSP_SelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            using (var retorno = ExecuteReader())
                return retorno.Read();
        }
        public Pagamento SelecionarDadosPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.CSSP_SelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            Pagamento retorno = new Pagamento();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new Pagamento
                    {
                        IdPagamento = reader.ReadAsInt("IdPagamento"),
                        DataPagamento = reader.ReadAsDateTime("DataPagamento"),                        
                        ValorPagamento = reader.ReadAsDecimal("ValorPagamento"),
                        Usuario = new Usuario
                        {
                            Cpf = reader.ReadAsString("Cpf"),
                            NomeUsuario = reader.ReadAsString("NomeUsuario")
                        }
                    };
            return retorno;
        }        
        public IEnumerable<Pagamento> ListarPagamentos()
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamento);
            return Listar();
        }
        public IEnumerable<Pagamento> ListarPagamentos(int mes)
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamento);
            AddParameter("@cpf", null);
            AddParameter("@mes", mes);
            return Listar();
        }
        public IEnumerable<Pagamento> ListarPagamentos(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamento);
            AddParameter("@cpf", cpf);
            return Listar();
        }
        public IEnumerable<Pagamento> ListarPagamentoSemana()
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamentoSemana);
            return Listar();
        }
        public IEnumerable<Pagamento> ListarPagamentoSemana(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamentoSemana);
            AddParameter("cpf", cpf);
            return Listar();
        }
        public IEnumerable<Pagamento> ListarPagamentoDia()
        {
            ExecuteProcedure(Procedures.CSSP_ListarPagamentoDia);
            return Listar();
        }

        private IEnumerable<Pagamento> Listar()
        {
            var retorno = new List<Pagamento>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                {
                    retorno.Add(new Pagamento
                    {
                        DataPagamento = reader.ReadAsDateTime("DataPagamento"),
                        IdPagamento = reader.ReadAsInt("IdPagamento"),
                        ValorPagamento = reader.ReadAsDecimal("ValorPagamento"),
                        Usuario = new Usuario()
                        {
                            Cpf = reader.ReadAsString("Cpf"),
                            NomeUsuario = reader.ReadAsString("NomeUsuario")
                        }
                    });
                }
            return retorno;
        }
    }
}
