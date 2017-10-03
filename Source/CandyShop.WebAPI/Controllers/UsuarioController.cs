﻿using CandyShop.Core.Services;
using CandyShop.Core.Services.Usuario;
using CandyShop.Core.Services.Usuario.Dto;
using System;
using System.Net;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly INotification _notification;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(INotification notification, IUsuarioRepository usuarioRepository,IUsuarioService usuarioService)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }


        public IHttpActionResult Post(UsuarioDto usuario)
        {
            try
            {
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());
                _usuarioRepository.InserirUsuario(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        [HttpPost, Route("api/Usuario/login")]
        public IHttpActionResult PostLogin(UsuarioDto usuario)
        {
            try
            {
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

                usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
                var user = _usuarioRepository.SelecionarUsuario(usuario.Cpf);
                if (user != null)
                {
                    return Ok(_usuarioService.VerificaLogin(usuario));
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.NotAcceptable, e.Message);
            }
        }

        public IHttpActionResult Get()
        {
            return Ok(_usuarioRepository.ListarUsuario());
        }

        [HttpGet, Route("api/Usuario/Devedores")] // Colocar quando controller tiver mais de um metodos GET
        public IHttpActionResult GetUsuariosDivida()
        {
            return Ok(_usuarioRepository.ListarUsuarioDivida());
        }

        [HttpGet, Route("api/Usuario/inativos")] // Colocar quando controller tiver mais de um metodos GET
        public IHttpActionResult GetUsuariosInativos()
        {
            return Ok(_usuarioRepository.ListarUsuarioInativo());
        }

        [HttpGet, Route("api/usuario/procurar/{nome}")]
        public IHttpActionResult GetPorNome(string nome)
        {
            return Ok(_usuarioRepository.ListarUsuarioPorNome(nome));
        }

        [HttpGet, Route("api/Usuario/saldo")]
        public IHttpActionResult GetSaldo()
        {
            return Ok(_usuarioRepository.VerificaCreditoLoja());
        }

        public IHttpActionResult Put(UsuarioDto usuario)
        {
            try
            {
                if (_notification.HasNotification())
                    return Content(HttpStatusCode.BadRequest, _notification.GetNotification());

                usuario.Cpf = usuario.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
                _usuarioRepository.EditarUsuario(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut,Route("api/usuario/desativar/{cpf}")]
        public IHttpActionResult PutDesativar(UsuarioDto usuario)
        {
            _usuarioRepository.DesativarUsuario(usuario.Cpf);
            return Ok();
        }

        [HttpGet, Route("api/Usuario/{cpf}/Detalhes")]
        public IHttpActionResult GetWithCpf(string cpf)
        {
            return Ok(_usuarioRepository.SelecionarUsuario(cpf));
        }
    }
}