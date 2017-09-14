using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace CandyShop.Application
{
    public class ProdutoApplication
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
    }
}
