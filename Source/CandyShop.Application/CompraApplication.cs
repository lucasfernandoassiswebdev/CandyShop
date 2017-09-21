using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application
{
    public class CompraApplication : ICompraInterface
    {
        private readonly string _enderecoApi = $"{ConfigurationManager.AppSettings["IP_API"]}/compra";

        public Response<string> InserirCompra(Compra compra)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, compra, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<string> InserirItens(Produto produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, produto, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<string> EditarCompra(Compra compra)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(_enderecoApi, compra, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<Compra>> ListaCompra()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/listacompra").Result;
                return new Response<IEnumerable<Compra>>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }

        public Response<IEnumerable<Compra>> ListaCompraPorCpf(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/listacompracpf").Result;
                return new Response<IEnumerable<Compra>>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }

        public Response<Compra> SelecionarCompra(int idcompra)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/selecionacompra").Result;
                return new Response<Compra>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }

        public Response<string> EditaItens(CompraProduto compraProduto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(_enderecoApi, compraProduto, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<Compra>> ListaCompraPorNome(string nomeUsuario)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/{nomeUsuario}").Result;
                return new Response<IEnumerable<Compra>>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }
    }
}