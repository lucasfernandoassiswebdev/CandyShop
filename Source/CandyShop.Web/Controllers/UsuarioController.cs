﻿using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using CandyShop.Web.Filters;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Image = System.Drawing.Image;

namespace CandyShop.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApplication _appUsuario;

        public UsuarioController(IUsuarioApplication usuario)
        {
            _appUsuario = usuario;
        }

        #region telas

        [AdminFilterResult]
        public ActionResult Index()
        {
            return View();
        }

        [AdminFilterResult]
        public ActionResult Cadastrar()
        {
            TempData["caminhoImagensUsuarios"] = "Imagens/Usuarios";
            return View();
        }

        [AdminFilterResult]
        public ActionResult Editar(string cpf, string telaAnterior)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);

            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro " + usuario.ContentAsString.First());
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensUsuarios"] = "Imagens/Usuarios";
            return View(usuario.Content);
        }

        [AdminFilterResult]
        public ActionResult Desativar(string cpf, string telaAnterior)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);
            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro " + usuario.ContentAsString.First());
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensUsuarios"] = "Imagens/Usuarios";
            return View(usuario.Content);
        }

        [AdminFilterResult]
        public ActionResult Detalhes(string cpf, string telaAnterior)
        {
            var response = _appUsuario.SelecionarUsuario(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString.First());
            ViewBag.telaAnterior = telaAnterior;
            TempData["caminhoImagensUsuarios"] = "Imagens/Usuarios";
            return View(response.Content);
        }
        #endregion

        #region listas

        [AdminFilterResult]
        public ActionResult Listar()
        {
            var response = _appUsuario.ListarUsuarios();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString.First());
            }
            TempData["nomeLista"] = "Usuários Ativos";
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarUsuariosEmDivida()
        {
            var response = _appUsuario.ListarUsuariosEmDivida();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString.First());
            }
            TempData["nomeLista"] = "Usuários em Dívida";
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ListarInativos()
        {
            var response = _appUsuario.ListarInativos();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString.First());
            }
            TempData["nomeLista"] = "Usuários Inativos";
            return View("Index", response.Content);
        }

        [AdminFilterResult]
        public ActionResult ProcurarUsuario(string nome)
        {
            var response = _appUsuario.ProcurarUsuario(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            TempData["nomeLista"] = "Usuários Relacionados";
            return View("Index", response.Content);
        }
        #endregion

        #region EXECUCOES
        [AdminFilterResult]
        [HttpPost]
        public ActionResult Cadastrar(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var response = _appUsuario.InserirUsuario(usuario);
                if (response.Status != HttpStatusCode.OK)
                    return Content($"{response.ContentAsString}");

                if (usuario.Imagem != null)
                {
                    string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
                    foreach (var prefixo in prefixos)
                    {
                        if (usuario.Imagem.StartsWith(prefixo))
                        {
                            usuario.Imagem = usuario.Imagem.Substring(prefixo.Length);

                            //transformando base64 em array de bytes
                            byte[] bytes = System.Convert.FromBase64String(usuario.Imagem);

                            Image imagem = (Bitmap)((new ImageConverter()).ConvertFrom(bytes));

                            //montando o nome e caminho de save da imagem
                            usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                            string caminho = $"Imagens/Usuarios/{usuario.Cpf}.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }

                return Content("Usuário cadastrado com sucesso");
            }
            return Content("Ops, ocorreu um erro ao editar usuário.");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            try
            {


                var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                var response = _appUsuario.EditarUsuario(usuario);

                if (response.Status != HttpStatusCode.OK)
                    return Content("Erro " + response.ContentAsString.First());

                if (usuario.Imagem != null)
                {
                    string[] prefixos = {"data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64,"};
                    foreach (var prefixo in prefixos)
                    {
                        if (usuario.Imagem.StartsWith(prefixo))
                        {
                            usuario.Imagem = usuario.Imagem.Substring(prefixo.Length);

                            byte[] bytes = System.Convert.FromBase64String(usuario.Imagem);

                            Image imagem = (Bitmap) ((new ImageConverter()).ConvertFrom(bytes));

                            usuario.Cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
                            string caminho = $"Imagens/Usuarios/{usuario.Cpf}.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }
                else
                {
                    var filePath = Server.MapPath("Imagens/Usuarios/" + cpf + ".jpg");
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                if (Session["Login"].ToString().Equals(cpf))
                {
                    var res = _appUsuario.SelecionarUsuario(cpf);
                    if (res.Status != HttpStatusCode.OK)
                        return Content("Erro ao atualizar seu saldo");
                    Session["saldoUsuario"] = res.Content.SaldoUsuario;
                }

                return Content("Edição concluída com sucesso!!");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [UserFilterResult]
        public ActionResult TrocarSenha(TrocaSenhaViewModel senhas)
        {
            if (senhas.ConfirmaNovaSenha.Equals(senhas.NovaSenha))
            {
                var usuario = new UsuarioViewModel
                {
                    Cpf = Session["Login"].ToString(),
                    SenhaUsuario = senhas.NovaSenha
                };
                var response = _appUsuario.TrocarSenha(usuario);
                if (response.Status != HttpStatusCode.OK)
                    return Content($"Erro ao trocar a senha, {response.ContentAsString}");
                return Content("Senha atualizada com sucesso");
            }
            return Content("Senhas não conferem");
        }

        [AdminFilterResult]
        [HttpPut]
        public ActionResult DesativarUsuario(UsuarioViewModel usuario)
        {
            var response = _appUsuario.DesativarUsuario(usuario);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro {response.Status}");
            return Content("Usuario desativado com sucesso!");
        }

        [UserFilterResult]
        public ActionResult Deslogar()
        {
            Session.Clear();
            return RedirectToAction("NavBar", "Home");
        }
        #endregion
    }
}