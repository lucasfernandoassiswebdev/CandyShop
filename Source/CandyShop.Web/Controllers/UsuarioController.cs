using CandyShop.Application.Interfaces;
using CandyShop.Application.ViewModels;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Editar(string cpf)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);

            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro " + usuario.ContentAsString.First());

            return View(usuario.Content);
        }

        public ActionResult Desativar(string cpf)
        {
            var usuario = _appUsuario.SelecionarUsuario(cpf);
            if (usuario.Status != HttpStatusCode.OK)
                return Content("Erro " + usuario.ContentAsString.First());
            return View(usuario.Content);
        }

        public ActionResult Detalhes(string cpf)
        {
            var response = _appUsuario.SelecionarUsuario(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content("Erro" + response.ContentAsString.First());
            return View(response.Content);
        }
        #endregion

        #region listas
        public ActionResult Listar()
        {
            var response = _appUsuario.ListarUsuarios();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString.First());
            }
            return View("Index", response.Content);
        }

        public ActionResult ListarUsuariosEmDivida()
        {
            var response = _appUsuario.ListarUsuariosEmDivida();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString.First());
            }
            return View("Index", response.Content);
        }

        public ActionResult ListarInativos()
        {
            var response = _appUsuario.ListarInativos();
            if (response.Status != HttpStatusCode.OK)
            {
                return Content("Erro " + response.ContentAsString.First());
            }
            return View("Index", response.Content);
        }

        public ActionResult ProcurarUsuario(string nome)
        {
            var response = _appUsuario.ProcurarUsuario(nome);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro: {response.Status}");
            return View("Index", response.Content);
        }
        #endregion

        #region execucoes
        [HttpPost]
        public ActionResult Cadastrar(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var response = _appUsuario.InserirUsuario(usuario);
                if (response.Status != HttpStatusCode.OK)
                    return Content($"Erro ao cadastrar usuario: {response.Status}");

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
                            string caminho = $"~/Imagens/{usuario.Cpf}.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }

                return Content("Usuário cadastrado com sucesso");
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPut]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var response = _appUsuario.EditarUsuario(usuario);

                if (response.Status != HttpStatusCode.OK)
                    return Content("Erro " + response.ContentAsString.First());

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
                            string caminho = $"~/Imagens/{usuario.Cpf}.jpg";

                            imagem.Save(Server.MapPath(caminho), ImageFormat.Jpeg);
                        }

                    }
                }

                return Content("Edição concluída com sucesso!!");

            }
            return View("Editar");
        }

        [HttpPost]
        public ActionResult DesativarUsuario(string cpf)
        {
            var response = _appUsuario.DesativarUsuario(cpf);
            if (response.Status != HttpStatusCode.OK)
                return Content($"Erro {response.Status}");
            return Content("Usuario desativado com sucesso!");
        }

        [HttpPost]
        public ActionResult Logar(UsuarioViewModel usuario)
        {
            var response = _appUsuario.VerificaLogin(usuario);
            if (!response.IsSuccessStatusCode)
                return new HttpStatusCodeResult(404, "Deu Pau");

            var model = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);

            if (model != 1)
            {
                return Content("Login ou senha incorretos");
            }

            Session["Login"] = "logado";
            return RedirectToAction("Padrao", "Home");
        }

        public ActionResult Deslogar()
        {
            Session.Clear();
            return RedirectToAction("Padrao", "Home");
        }
        #endregion
    }
}