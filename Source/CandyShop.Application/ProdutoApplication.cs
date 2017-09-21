using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly string _enderecoApi = $"{ConfigurationManager.AppSettings["IP_API"]}/produto";

        public Response<IEnumerable<ProdutoViewModel>> ListarProdutos()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<ProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> InserirProduto(ProdutoViewModel produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, produto, new JsonMediaTypeFormatter()).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                    return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
                return new Response<string>(response.StatusCode);
            }
        }

        public Response<ProdutoViewModel> DetalharProduto(int idProduto)
        {
            using (var cliente = new HttpClient())
            {
                var response = cliente.GetAsync($"{_enderecoApi}/selecionar/{idProduto}").Result;
                return  new Response<ProdutoViewModel>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> EditarProduto(ProdutoViewModel produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(_enderecoApi, produto, new JsonMediaTypeFormatter()).Result;
                if(response.StatusCode != HttpStatusCode.OK)
                    return  new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);

                return new Response<string>(response.StatusCode);
            }
        }

        public Response<string> DesativarProduto(int idProduto)
        {
            using (var cliente = new HttpClient())
            {
                var response = cliente.DeleteAsync($"{_enderecoApi}/desativar/{idProduto}").Result;
                if (response.StatusCode != HttpStatusCode.OK)
                    return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
                return new Response<string>(response.StatusCode);                
            }
        }

        public Response<IEnumerable<ProdutoViewModel>> ListarInativos()
        {
            using (var cliente = new HttpClient())
            {
                var response = cliente.GetAsync($"{_enderecoApi}/inativos").Result;
                return new Response<IEnumerable<ProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<ProdutoViewModel>> ProcurarProduto(string nome)
        {
            using (var cliente = new HttpClient())
            {
                var response = cliente.GetAsync($"{_enderecoApi}/procurar/{nome}").Result;
                return new Response<IEnumerable<ProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
