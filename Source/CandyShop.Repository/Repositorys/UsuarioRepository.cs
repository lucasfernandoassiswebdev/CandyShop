using CandyShop.Core.Services.Usuario;
using CandyShop.Repository.Database;
using CandyShop.Repository.DataBase;
using System.Collections.Generic;

namespace CandyShop.Repository.Repositorys
{

    public class UsuarioRepository : Execucao, IUsuarioRepository
    {
        public UsuarioRepository(Conexao conexao) : base(conexao)
        {

        }

        private enum Procedures
        {
            CSSP_InsUsuario,
            CSSP_UpdUsuario,
            CSSP_SelUsuario,
            CSSP_LisUsuario,
            CSSP_SelUsuariosDivida,
            CSSP_DesUsuario,
            CSSP_LisUsuarioIgual,
            CSSP_ListarUsuariosInativos,
            CSSP_LisUsuarioPorNome,
            CSSP_VerificaLoginSenha,
            CSSP_VerificaSaldoLoja,
            CSSP_UpdSenha
        }

        public void InserirUsuario(Usuario usuario)
        {
            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            {
                ExecuteProcedure(Procedures.CSSP_InsUsuario);
                AddParameter("@NomeUsuario", usuario.NomeUsuario);
                AddParameter("@SenhaUsuario", usuario.SenhaUsuario);
                AddParameter("@SaldoUsuario", usuario.SaldoUsuario);
                AddParameter("@CpfUsuario", cpf);
                AddParameter("@Ativo", usuario.Ativo);
                AddParameter("@Classificacao", usuario.Classificacao);

                ExecuteNonQuery();
            }
        }

        public void EditarUsuario(Usuario usuario)
        {
            ExecuteProcedure(Procedures.CSSP_UpdUsuario);
            AddParameter("@Cpf", usuario.Cpf);
            AddParameter("@NomeUsuario", usuario.NomeUsuario);
            AddParameter("@SenhaUsuario", usuario.SenhaUsuario);
            AddParameter("@SaldoUsuario", usuario.SaldoUsuario);
            AddParameter("@Ativo", usuario.Ativo);
            AddParameter("@Classificacao", usuario.Classificacao);

            ExecuteNonQuery();
        }

        public void TrocarSenha(Usuario usuario)
        {
            ExecuteProcedure(Procedures.CSSP_UpdSenha);
            AddParameter("@cpf", usuario.Cpf);
            AddParameter("@senha", usuario.SenhaUsuario);
            ExecuteNonQuery();
        }

        public void DesativarUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_DesUsuario);
            AddParameter("@Cpf", cpf);

            ExecuteNonQuery();
        }

        public Usuario SelecionarUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_SelUsuario);
            AddParameter("@Cpf", cpf);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return new Usuario()
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Classificacao = reader.ReadAsString("Classificacao")
                    };
            return null;
        }

        public IEnumerable<Usuario> ListarUsuario()
        {
            ExecuteProcedure(Procedures.CSSP_LisUsuario);            
            return Listar();
        }

        public IEnumerable<Usuario> ListarUsuarioDivida()
        {
            ExecuteProcedure(Procedures.CSSP_SelUsuariosDivida);           
            return Listar();
        }

        public IEnumerable<Usuario> ListarUsuarioInativo()
        {
            ExecuteProcedure(Procedures.CSSP_ListarUsuariosInativos);            
            return Listar();
        }

        public Usuario SelecionarDadosUsuario(string cpf)
        {
            ExecuteProcedure(Procedures.CSSP_SelUsuario);
            AddParameter("@CpfUsuario", cpf);
            var retorno = new Usuario();
            using (var reader = ExecuteReader())
                if (reader.Read())
                    retorno = new Usuario
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Classificacao = reader.ReadAsString("Classificacao")
                    };
            return retorno;
        }

        public int VericaUsuarioIgual(Usuario usuario)
        {
            ExecuteProcedure(Procedures.CSSP_LisUsuarioIgual);
            AddParameter("@cpf", usuario.Cpf);
            AddParameter("@nome", usuario.NomeUsuario);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return 1;
            return 0;
        }

        public IEnumerable<Usuario> ListarUsuarioPorNome(string nome)
        {
            ExecuteProcedure(Procedures.CSSP_LisUsuarioPorNome);
            AddParameter("@NomeUsuario", nome);
            return Listar();
        }

        public int VerificaLogin(Usuario usuario)
        {
            ExecuteProcedure(Procedures.CSSP_VerificaLoginSenha);
            AddParameter("@Cpf", usuario.Cpf);
            AddParameter("@SenhaUsuario", usuario.SenhaUsuario);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return 1;
            return 0;
        }

        public decimal VerificaCreditoLoja()
        {
            ExecuteProcedure(Procedures.CSSP_VerificaSaldoLoja);
            using (var reader = ExecuteReader())
                if (reader.Read())
                    return reader.ReadAsDecimal("saldo");
            return 0;
        }

        private IEnumerable<Usuario> Listar()
        {
            var retorno = new List<Usuario>();
            using (var reader = ExecuteReader())
                while (reader.Read())
                    retorno.Add(new Usuario
                    {
                        Cpf = reader.ReadAsString("Cpf"),
                        SenhaUsuario = reader.ReadAsString("SenhaUsuario"),
                        SaldoUsuario = reader.ReadAsDecimal("SaldoUsuario"),
                        NomeUsuario = reader.ReadAsString("NomeUsuario"),
                        Ativo = reader.ReadAsString("Ativo"),
                        Classificacao = reader.ReadAsString("Classificacao")
                    });
            return retorno;
        }
    }
}
