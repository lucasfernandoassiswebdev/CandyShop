using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application.Applications
{
    public class PagamentoApplication : IPagamentoApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.enderecoApi}/pagamento";

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(string cpf,string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/{cpf}").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentos(int mes,string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/mes/{mes}").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana(string cpf,string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/semana/{cpf}").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosSemana(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/semana").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/dia").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<PagamentoViewModel>> ListarPagamentosDia(DateTime dia,string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/dia/{dia}").Result;
                return new Response<IEnumerable<PagamentoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<PagamentoViewModel> SelecionarPagamento(int idPagamento,string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token,client);
                var response = client.GetAsync($"{_enderecoApi}/id/{idPagamento}").Result;
                return new Response<PagamentoViewModel>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> InserirPagamento(PagamentoViewModel pagamento,string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token,client);
                var response = client.PostAsync(_enderecoApi, pagamento, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                   ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                   : new Response<string>(response.StatusCode);
            }
        }

        public Response<string> EditarPagamento(PagamentoViewModel pagamento,string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PutAsync(_enderecoApi, pagamento, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        private static void AtualizaToken(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}
