using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application
{
    public class PagamentoApplication : IPagamentoApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.enderecoApi}/pagamento";

        #region gets

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

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(int mes)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/mes/{mes}").Result;
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

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/dia").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia(DateTime dia)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/dia/{dia}").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        #endregion


        public Response<PagamentoViewModel> DetalharPagamento(int idPagamento)
        {
            throw new NotImplementedException();
        }

        public Response<string> InserirPagamento(PagamentoViewModel pagamento)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, pagamento, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK 
                   ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode) 
                   :new Response<string>(response.StatusCode);
            }
        }                
    }
}
