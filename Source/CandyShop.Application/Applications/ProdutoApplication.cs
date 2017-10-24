using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application.Applications
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.enderecoApi}/Produto";
        private readonly string _enderecoApiUnauthorized = $"{ApiConfig.enderecoApi}/ProdutoUnauthorized";

        public Response<string> InserirProduto(ProdutoViewModel produto, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token,client);
                var response = client.PostAsync(_enderecoApi, produto, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> EditarProduto(ProdutoViewModel produto, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PutAsync(_enderecoApi, produto, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                      ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                      : new Response<string>(response.StatusCode);
            }
        }
        public Response<string> DesativarProduto(ProdutoViewModel produto, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PutAsync($"{_enderecoApi}/desativar/{produto.IdProduto}", produto, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                     ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                     : new Response<string>(response.StatusCode);
            }
        }

        public Response<ProdutoViewModel> DetalharProduto(int idProduto)
        {
            using (var cliente = new HttpClient())
            {
                var response = cliente.GetAsync($"{_enderecoApiUnauthorized}/selecionar/{idProduto}").Result;
                return new Response<ProdutoViewModel>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<ProdutoViewModel>> ListarProdutos()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApiUnauthorized).Result;
                return new Response<IEnumerable<ProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<ProdutoViewModel>> ListarInativos(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/inativos").Result;
                return new Response<IEnumerable<ProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<ProdutoViewModel>> ProcurarProduto(string nome)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApiUnauthorized}/procurar/{nome}").Result;
                return new Response<IEnumerable<ProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<ProdutoViewModel>> ListarCategoria(string categoria)
        {
            using (var cliente = new HttpClient())
            {
                var response = cliente.GetAsync($"{_enderecoApiUnauthorized}/categoria/{categoria}").Result;
                return new Response<IEnumerable<ProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        private static void AtualizaToken(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}
