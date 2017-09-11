
﻿using CandyShop.Core;
using CandyShop.Core.Usuario.Dto;
using Concessionaria.Repositorio;
using System.Collections.Generic;

namespace CandyShop.Repository
{

    public class UsuarioRepository : ConnectDB, IUsuarioRepository
    {


        private enum Procedures
        {
            GCS_InsUsuario,
            GCS_DelUsuario,
            GCS_UpdUsuario,
            GCS_SelUsuario,
            GCS_LisUsuario
        }

        public void InserirUsuario(UsuarioDto usuario)
        {
            ExecuteProcedure(Procedures.GCS_InsUsuario);
            AddParameter("@NomeUsuario", usuario.NomeUsuario);
            AddParameter("@SenhaUsuario", usuario.SenhaUsuario);
            AddParameter("@SaldoUsuario", usuario.SaldoUsuario);
            AddParameter("@CpfUsuario", usuario.Cpf);

            ExecuteNonQuery();
        }

        public void EditarUsuario(UsuarioDto usuario)
        {
            ExecuteProcedure(Procedures.GCS_UpdUsuario);
            AddParameter("@Cpf", usuario.Cpf);
            AddParameter("@NomeUsuario", usuario.NomeUsuario);
            AddParameter("@SenhaUsuario", usuario.SenhaUsuario);
            AddParameter("@SaldoUsuario", usuario.SaldoUsuario);

            ExecuteNonQuery();
        }

        public void DeletarUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_DelUsuario);
            AddParameter("@Cpf", cpf);
            ExecuteNonQuery();
        }

        public bool SelecionarUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_SelUsuario);
            AddParameter("@Cpf", cpf);
            using (var retorno = ExecuteReader())
                return retorno.Read();
        }

        public IEnumerable<UsuarioDto> ListarUsuario()
        {
            ExecuteProcedure(Procedures.GCS_LisUsuario);
            var retorno = new List<UsuarioDto>();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    do
                    {
                        retorno.Add(new UsuarioDto()
                        {
                            Cpf = reader.ReadAsString("Cpf"),
                            SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                            SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                            NomeUsuario = reader.ReadAsString("NomeUsuario")
                        });
                    } while (reader.Read());

            return retorno;
        }

        public UsuarioDto SelecionarDadosUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.GCS_SelUsuario);
            AddParameter("@CpfUsuario", cpf);
            var retorno = new UsuarioDto();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new UsuarioDto
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario")
                    };
            return retorno;
        }
    }
}
