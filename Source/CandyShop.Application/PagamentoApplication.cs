using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace CandyShop.Application
{
    public class PagamentoApplication : IPagamentoApplication
    {
        private readonly string _enderecoApi = $"{ConfigurationManager.AppSettings["IP_API"]}/pagamento";

        public Response<IEnumerable<Pagamento>> ListarPagamentos()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<Pagamento>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<Pagamento>> ListarPagamentosCpf(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/cpf/{cpf}").Result;
                return new Response<IEnumerable<Pagamento>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<Pagamento> DetalharPagamento(int idPagamento)
        {
            throw new NotImplementedException();
        }

        public Response<string> InserirPagamento(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }

        
    }
}
