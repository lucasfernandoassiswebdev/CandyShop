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
        private readonly string _enderecoApi = $"{ConfigurationManager.AppSettings["IP_API"]}/usuario";

        public Response<string> InserirUsuario(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(_enderecoApi, usuario, new JsonMediaTypeFormatter()).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                    return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);

                return new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<Usuario>> ListarUsuarios()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<Usuario>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<IEnumerable<Usuario>> ListarUsuariosEmDivida()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<Usuario>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<Usuario> SelecionarUsuario(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/Detalhes/{cpf}").Result;
                return new Response<Usuario>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
