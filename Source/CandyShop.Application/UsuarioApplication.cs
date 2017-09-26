using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace CandyShop.Application
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly string _enderecoApi = $"{ConfigurationManager.AppSettings["IP_API"]}/Usuario";

        public Response<string> InserirUsuario(UsuarioViewModel usuario)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, usuario, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode) : new Response<string>(response.StatusCode);
            }
        }

        public Response<string> EditarUsuario(UsuarioViewModel usuario)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(_enderecoApi, usuario, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK 
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode) 
                    : new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<UsuarioViewModel>> ListarUsuarios()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<UsuarioViewModel>> ListarUsuariosEmDivida()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/Devedores").Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<UsuarioViewModel>> ListarInativos()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/inativos").Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<UsuarioViewModel> SelecionarUsuario(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/{cpf}/Detalhes").Result;
                return new Response<UsuarioViewModel>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> DesativarUsuario(UsuarioViewModel usuario)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync($"{_enderecoApi}/desativar/{usuario.Cpf}", usuario, new JsonMediaTypeFormatter()).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                    return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
                return new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<UsuarioViewModel>> ProcurarUsuario(string nome)
        {
            using (var cliente = new HttpClient())
            {
                var response = cliente.GetAsync($"{_enderecoApi}/procurar/{nome}").Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public HttpResponseMessage VerificaLogin(UsuarioViewModel usuario)
        {
            using (var client = new HttpClient())
            {
                return client.PostAsync($"{_enderecoApi}/login",usuario, new JsonMediaTypeFormatter()).Result;
            }
        }

        public Response<decimal> VerificaCreditoLoja()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/saldo").Result;
                if (response.StatusCode != HttpStatusCode.OK)
                    return new Response<decimal>(response.StatusCode);

                return new Response<decimal>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
