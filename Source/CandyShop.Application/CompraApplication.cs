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

        public Response<string> InserirCompra(CompraViewModel compra)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, compra, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<string> InserirItens(ProdutoViewModel produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, produto, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<string> EditarCompra(CompraViewModel compra)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(_enderecoApi, compra, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<CompraViewModel>> ListaCompra()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/listacompra").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }

        public Response<IEnumerable<CompraViewModel>> ListaCompraPorCpf(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/listacompracpf").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }

        public Response<CompraViewModel> SelecionarCompra(int idcompra)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/selecionacompra").Result;
                return new Response<CompraViewModel>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }

        public Response<string> EditaItens(CompraProdutoViewModel compraProduto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(_enderecoApi, compraProduto, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<CompraViewModel>> ListaCompraPorNome(string nomeUsuario)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/{nomeUsuario}").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result,response.StatusCode);
            }
        }
    }
}