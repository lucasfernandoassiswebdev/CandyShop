namespace CandyShop.Core.Services.Pagamento
{
    public class PagamentoService : IPagamentoService
    {
        private readonly INotification _notification;

        public PagamentoService(INotification notification)
        {
            _notification = notification;
        }

        public void ValidarPagamento(Pagamento pagamento)
        {
            if (pagamento.ValorPagamento < 0)
            {
                _notification.Add("Valor inválido!");
            }
        }
    }
}
