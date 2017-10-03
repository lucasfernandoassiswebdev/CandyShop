using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Net.Http;

namespace CandyShop.Application
{
    public class CompraProdutoApplication : ICompraProdutoApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.enderecoApi}/CompraProduto";

        public Response<IEnumerable<CompraProdutoViewModel>> ListarProdutos(int idCompra)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/{idCompra}").Result;
                return new Response<IEnumerable<CompraProdutoViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
