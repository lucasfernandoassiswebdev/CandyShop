
using CandyShop.Core;
using CandyShop.Core.Usuario.Dto;
using System.Collections.Generic;

namespace CandyShop.Repository
{

    public class UsuarioRepository : ConnectDB, IUsuarioRepository
    {
        private enum Procedures
        {
            CSSP_InsUsuario,
            CSSP_UpdUsuario,
            CSSP_SelUsuario,
            CSSP_LisUsuario,
            CSSP_SelUsuariosDivida,
            CSSP_DesUsuario,
            CSSP_LisUsuarioIgual
        }

        public void InserirUsuario(UsuarioDto usuario)
        {
            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");

            ExecuteProcedure(Procedures.CSSP_InsUsuario);
            AddParameter("@NomeUsuario", usuario.NomeUsuario);
            AddParameter("@SenhaUsuario", usuario.SenhaUsuario);
            AddParameter("@SaldoUsuario", usuario.SaldoUsuario);
            AddParameter("@CpfUsuario", cpf);
            AddParameter("@Ativo",usuario.Ativo);

            ExecuteNonQuery();
        }

        public void EditarUsuario(UsuarioDto usuario)
        {
            ExecuteProcedure(Procedures.CSSP_UpdUsuario);
            AddParameter("@Cpf", usuario.Cpf);
            AddParameter("@NomeUsuario", usuario.NomeUsuario);
            AddParameter("@SenhaUsuario", usuario.SenhaUsuario);
            AddParameter("@SaldoUsuario", usuario.SaldoUsuario);
            AddParameter("@Ativo", usuario.Ativo);

            ExecuteNonQuery();
        }

        public void DesativarUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_DesUsuario);
            AddParameter("@Cpf", cpf);
         
            ExecuteNonQuery();
        }

        public UsuarioDto SelecionarUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_SelUsuario);
            AddParameter("@Cpf", cpf);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new UsuarioDto()
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario"),
                        Ativo = reader.ReadAsString("Ativo")
                    };
            return null;
        }

        public IEnumerable<UsuarioDto> ListarUsuario()
        {
            ExecuteProcedure(Procedures.CSSP_LisUsuario);
            var retorno = new List<UsuarioDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new UsuarioDto()
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario"),
                        Ativo = reader.ReadAsString("Ativo")
                    });
            return retorno;
        }

        public IEnumerable<UsuarioDto> ListarUsuarioDivida()
        {
            ExecuteProcedure(Procedures.CSSP_SelUsuariosDivida);
            var retorno = new List<UsuarioDto>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new UsuarioDto()
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario"),
                        Ativo = reader.ReadAsString("Ativo")
                    });
            return retorno;
        }

        public UsuarioDto SelecionarDadosUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_SelUsuario);
            AddParameter("@CpfUsuario", cpf);
            var retorno = new UsuarioDto();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new UsuarioDto
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario"),
                        Ativo = reader.ReadAsString("Ativo")
                    };
            return retorno;
        }

        public int VericaUsuarioIgual(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_LisUsuarioIgual);
            AddParameter("@cpf", cpf);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return 1;
            return 0;
        }
    }
}
