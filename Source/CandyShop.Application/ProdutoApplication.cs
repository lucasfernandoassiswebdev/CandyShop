using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly string _enderecoApi = $"{ConfigurationManager.AppSettings["IP_API"]}/produto";

        public Response<IEnumerable<Produto>> ListarProdutos()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<Produto>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> InserirProduto(Produto produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, produto, new JsonMediaTypeFormatter()).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
                }
                return new Response<string>("Inserção feita com sucesso!", response.StatusCode);
            }
        }
    }
}
