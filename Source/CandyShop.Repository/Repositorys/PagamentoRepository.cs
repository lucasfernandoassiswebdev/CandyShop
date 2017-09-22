using CandyShop.Core.Services.Pagamento;
using CandyShop.Core.Services.Pagamento.Dto;
using CandyShop.Core.Services.Usuario.Dto;
using CandyShop.Repository.Database;
using System;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys
{
    public class PagamentoRepository : ConnectDB, IPagamentoRepository
    {
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

        public void InserirPagamento(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.CSSP_InsPagamento);
            AddParameter("@Cpf", pagamento.Usuario.Cpf);
            AddParameter("@ValorPagamento", pagamento.ValorPagamento);
            ExecuteNonQuery();
        }

        public void EditarPagamento(PagamentoDto pagamento)
        {
            ExecuteProcedure(Procedures.CSSP_UpdPagamento);
            AddParameter("@IdPagamento", pagamento.IdPagamento);
            AddParameter("@DataPagamento", pagamento.DataPagamento);
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

        public PagamentoDto SelecionarDadosPagamento(int idPagamento)
        {
            ExecuteProcedure(Procedures.CSSP_SelPagamento);
            AddParameter("@IdPagamento", idPagamento);
            PagamentoDto retorno = new PagamentoDto();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new PagamentoDto
                    {
                        DataPagamento = reader.ReadAsDateTime("DataPagamento"),
                        IdPagamento = reader.ReadAsInt("IdPagamento"),
                        ValorPagamento = reader.ReadAsDecimal("ValorPagamento"),
                        Usuario = new UsuarioDto
                        {
                            Cpf = reader.ReadAsString("Cpf")
                        }
                    };
            return retorno;
        }        

        public IEnumerable<PagamentoDto> ListarPagamentos()
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamento);
            return Listar();
        }

        public IEnumerable<PagamentoDto> ListarPagamentos(int mes)
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamento);
            AddParameter("@cpf", null);
            AddParameter("@mes", mes);
            return Listar();
        }

        public IEnumerable<PagamentoDto> ListarPagamentos(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamento);
            AddParameter("@cpf", cpf);
            return Listar();
        }

        public IEnumerable<PagamentoDto> ListarPagamentoSemana()
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamentoSemana);
            return Listar();
        }
        
        public IEnumerable<PagamentoDto> ListarPagamentoSemana(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisPagamentoSemana);
            AddParameter("cpf", cpf);
            return Listar();
        }

        public IEnumerable<PagamentoDto> ListarPagamentoDia()
        {
            ExecuteProcedure(Procedures.CSSP_ListarPagamentoDia);
            return Listar();
        }

        public IEnumerable<PagamentoDto> ListarPagamentoDia(DateTime data)
        {
            ExecuteProcedure(Procedures.CSSP_ListarPagamentoDia);
            AddParameter("@data", data);
            return Listar();
        }

        private IEnumerable<PagamentoDto> Listar()
        {
            var retorno = new List<PagamentoDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                {
                    retorno.Add(new PagamentoDto
                    {
                        DataPagamento = reader.ReadAsDateTime("DataPagamento"),
                        IdPagamento = reader.ReadAsInt("IdPagamento"),
                        ValorPagamento = reader.ReadAsDecimal("ValorPagamento"),
                        Usuario = new UsuarioDto()
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
