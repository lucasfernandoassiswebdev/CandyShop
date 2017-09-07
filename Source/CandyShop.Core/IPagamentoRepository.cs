using CandyShop.Core.Pagamento.Dto;
using System.Collections;

namespace CandyShop.Core
{
    public interface IPagamentoRepository
    {
        void Inserir(PagamentoDto entidade);
        void Editar(PagamentoDto entidade);
        void Excluir(int idPagamento);
        IEnumerable ListarPagamentos();
        PagamentoDto ListarPagamentoPorCpf(string cpf);

    }
}
