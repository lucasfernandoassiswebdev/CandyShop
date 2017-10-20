using System.Collections.Generic;

namespace CandyShop.Core.Services.Compra
{
    public interface ICompraRepository
    {
        int InserirCompra(Compra compra, out int sequencial);
        int SelecionarCompra(int idCompra);
        Compra SelecionarDadosCompra(int idCompra);        
        IEnumerable<Compra> ListarCompra();
        IEnumerable<Compra> ListarCompraPorNome(string nome);        
        IEnumerable<Compra> ListarCompraPorCpf(string cpf);
        IEnumerable<Compra> ListarCompraSemana();
        IEnumerable<Compra> ListarCompraMes(int mes);
        IEnumerable<Compra> ListarCompraDia();
        void CommitTransaction();
        void RollBackTransaction();
        void BeginTransaction();
    }
}
