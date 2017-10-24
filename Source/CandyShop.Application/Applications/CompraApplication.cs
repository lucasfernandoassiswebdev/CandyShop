using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application.Applications
{
    public class CompraApplication : ICompraApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.enderecoApi}/compra";

        public Response<int> InserirCompra(CompraViewModel compra, string token)
        {
            using (var client = new HttpClient())
            {
                /* Aqui é definido o endereço da API, o verbo HTTP que será utilizado
                   (nesse caso POST) e o tipo de retorno(nesse caso JSON), a linha de baixo
                   define o tipo da resposta(nesse caso INT) e recebe o status dessa resposta
                   (ok, bad request, internal server error, etc) */
                AtualizaToken(token, client);
                var response = client.PostAsync(_enderecoApi, compra, new JsonMediaTypeFormatter()).Result;
                return new Response<int>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<CompraViewModel> SelecionarCompra(int idCompra, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/selecionarcompra/{idCompra}").Result;
                return new Response<CompraViewModel>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<CompraViewModel>> ListaCompra(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<CompraViewModel>> ListaCompraPorCpf(string cpf, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/listaCompracpf/{cpf}").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<CompraViewModel>> ListarComprasSemana(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/semana").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<CompraViewModel>> ListarComprasMes(int mes, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/mes/{mes}").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<CompraViewModel>> ListarComprasDia(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/dia").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<CompraViewModel>> ListaCompraPorNome(string nomeUsuario, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/{nomeUsuario}").Result;
                return new Response<IEnumerable<CompraViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        private static void AtualizaToken(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}