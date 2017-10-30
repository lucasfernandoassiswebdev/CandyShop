﻿using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System.Net;
using System.Web.Mvc;

namespace CandyShop.Web.Controllers.Usuario
{
    [UserFilterResult]
    public class UsuarioComumController : Controller
    {
        private readonly IUsuarioApplication _appUsuario;

        public UsuarioComumController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
        }

        public ActionResult Deslogar()
        {
            Session.Clear();
            return RedirectToAction("NavBar", "Home");
        }

        [HttpPost]
        public ActionResult TrocarSenha(TrocaSenhaViewModel senhas, string token)
        {
            if(senhas.NovaSenha == null || senhas.ConfirmaNovaSenha == null)
                return Content("Campos não podem ser vazios!!!!");

            if (!senhas.ConfirmaNovaSenha.Equals(senhas.NovaSenha))
                return Content("Senhas não conferem");

            var usuario = new UsuarioViewModel
            {
                Cpf = Session["Login"].ToString(),
                SenhaUsuario = senhas.NovaSenha
            };
            var response = _appUsuario.TrocarSenha(usuario, token);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro ao trocar a senha, {response.ContentAsString}");

            if (Session["FirstLogin"].ToString().Equals("T"))
                Session["FirstLogin"] = "F";
            return Content("Senha alterada com sucesso!");

        }
    }
}