using CandyShop.Core.Usuario.Dto;
using System.Collections;

namespace CandyShop.Repository
{
    public class UsuarioRepository : ConnectDB
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

        public void Editarusuario(UsuarioDto usuario)
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

        public IEnumerable ListarUsuario()
        {
            ExecuteProcedure(Procedures.GCS_LisUsuario);
            return ExecuteReader();
        }
    }
}
