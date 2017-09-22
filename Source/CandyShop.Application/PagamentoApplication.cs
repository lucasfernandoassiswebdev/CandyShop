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

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentos()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/{cpf}").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/semana").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/semana/{cpf}").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<PagamentoViewModel> DetalharPagamento(int idPagamento)
        {
            throw new NotImplementedException();
        }

        public Response<string> InserirPagamento(PagamentoViewModel pagamento)
        {
            throw new NotImplementedException();
        }                
    }
}
