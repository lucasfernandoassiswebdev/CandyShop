using CandyShop.Core.Services.Pagamento.Dto;

namespace CandyShop.Core.Services.Pagamento
{
    public interface IPagamentoService
    {
        void ValidarPagamento(PagamentoDto pagamento);
    }
}
