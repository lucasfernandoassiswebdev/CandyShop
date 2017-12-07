using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System;

namespace CandyShop.Application.Applications
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.enderecoApi}/Usuario";
        private readonly string _enderecoApiUnauthorized = $"{ApiConfig.enderecoApi}/UsuarioUnauthorized";

        public Response<string> InserirUsuario(UsuarioViewModel usuario, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PostAsync(_enderecoApi, usuario, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> EditarUsuario(UsuarioViewModel usuario, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PutAsync(_enderecoApi, usuario, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }
        public Response<string> TrocarSenha(UsuarioViewModel usuario, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PutAsync($"{_enderecoApi}/trocarSenha", usuario, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK
                    ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode)
                    : new Response<string>(response.StatusCode);
            }
        }
        public Response<string> DesativarUsuario(UsuarioViewModel usuario, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PutAsync($"{_enderecoApi}/desativar/{usuario.Cpf}", usuario, new JsonMediaTypeFormatter()).Result;
                return response.StatusCode != HttpStatusCode.OK ? new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode) : new Response<string>(response.StatusCode);
            }
        }

        public Response<IEnumerable<UsuarioViewModel>> ListarUsuarios(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync(_enderecoApi).Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<UsuarioViewModel>> ListarUsuariosEmDivida(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/Devedores").Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<UsuarioViewModel>> ListarInativos(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/inativos").Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<UsuarioViewModel> SelecionarUsuario(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApiUnauthorized}/{cpf}/Detalhes").Result;
                return new Response<UsuarioViewModel>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<UsuarioViewModel> SelecionarUsuario(string cpf, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/{cpf}/Detalhes").Result;
                return new Response<UsuarioViewModel>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<IEnumerable<UsuarioViewModel>> ProcurarUsuario(string nome, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/procurar/{nome}").Result;
                return new Response<IEnumerable<UsuarioViewModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
        public Response<string> VerificaLogin(UsuarioViewModel usuario)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{_enderecoApiUnauthorized}/login", usuario, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> VerificaEmailExiste(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApiUnauthorized}/{cpf}/VerificaEmailExiste").Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<decimal> VerificaCreditoLoja(string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.GetAsync($"{_enderecoApi}/saldo").Result;
                return response.StatusCode != HttpStatusCode.OK ? new Response<decimal>(response.StatusCode) : new Response<decimal>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> CadastraEmail(UsuarioViewModel usuario, string token)
        {
            using (var client = new HttpClient())
            {
                AtualizaToken(token, client);
                var response = client.PutAsync($"{_enderecoApi}/CadastraEmail", usuario, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        private static void AtualizaToken(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }

       
    }
}

