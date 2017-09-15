﻿using CandyShop.Application;
using CandyShop.Application.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioApplication _appUsuario = new UsuarioApplication();

        //public UsuarioController(IUsuarioApplication usuario)
        //{
        //    _appUsuario = usuario;
        //}

        public ActionResult Index()
        {
            var response = _appUsuario.ListarUsuarios();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString);
            }
            return View(response.Content);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Cadastrar(Usuario usuario)
        {
            var response = _appUsuario.InserirUsuario(usuario);
            return Content(response.ContentAsString);
        }

        public ActionResult Editar()
        {
            return View();
        }
    }
}