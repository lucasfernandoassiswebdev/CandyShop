using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace CandyShop.Core.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly INotification _notification;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(INotification notification, IUsuarioRepository usuarioRepository)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
        }

        public void InserirUsuario(Usuario usuario)
        {
            if (!usuario.VerificaInsercao(_notification))
                return;

            //verificando se não está sendo cadastrado um cpf repetido
            var usuarios = _usuarioRepository.ListarUsuario();
            var cpf = usuario.Cpf.Replace(".", "").Replace("-", "");
            if (usuarios.Any(usuarioA => usuarioA.Cpf == cpf))
            {
                _notification.Add("Este Cpf já existe!");
                return;
            }

            _usuarioRepository.InserirUsuario(usuario);
        }

        public void EditarUsuario(Usuario usuario)
        {
            if (!usuario.VerificaEdicao(_notification))
                return;
            _usuarioRepository.EditarUsuario(usuario);
        }

        public int VerificaLogin(Usuario usuario)
        {
            return _usuarioRepository.VerificaLogin(usuario) == 1 ? 1 : 0;
        }

        public void AlteraSenha(string cpf)
        {
            var email = _usuarioRepository.VerificaEmailExiste(cpf);
            if (!string.IsNullOrEmpty(email))
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var stringChars = new char[6];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                    stringChars[i] = chars[random.Next(chars.Length)];

                var senhaGerada = new string(stringChars);
                var usuario = new Usuario
                {
                    Cpf = cpf,
                    SenhaUsuario = senhaGerada
                };

                _usuarioRepository.TrocarSenha(usuario);

                SmtpClient cliente = new SmtpClient();
                NetworkCredential credenciais = new NetworkCredential();

                // definir as configuraçoes do cliente
                cliente.Host = "smtp.gmail.com";
                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                cliente.UseDefaultCredentials = false;

                //definir as credenciais de acesso ao email
                credenciais.UserName = "candyshopsmn";
                credenciais.Password = "candyshopsmn2017";

                //define as credenciais no cliente
                cliente.Credentials = credenciais;

                //preparar a mensagem a enviar
                MailMessage mensagem = new MailMessage();

                //quem envio
                mensagem.From = new MailAddress("candyshopsmn@gmail.com");

                //assunto
                mensagem.Subject = "Redefinição de senha";

                //PARA por por codigo html
                mensagem.IsBodyHtml = true;

                //anexo
                // mensagem.Attachments.Add(new Attachment(lbl_anexo.Text));
                //texto //corpo da mensagem
                mensagem.Body = "Sua nova senha é: " + senhaGerada;

               

                try
                {
                    //para quem vai a mensagem
                    mensagem.To.Add(email);

                    //envio de mensagem de email(finalemnte)
                    cliente.Send(mensagem);

                }
                catch 
                {
                    _notification.Add("Falha ao enviar email, contate um administrador");
                }

                return;
            }
            _notification.Add("Email não encontrado para este cpf, contate o administrador");
        }

        public void VerificaSenha(string novaSenha)
        {
            if (novaSenha == null)
                _notification.Add("A senha não pode estar vazia!");
        }

        public void CadastraEmail(string novoEmail, string cpf)
        {
            if (cpf != string.Empty && novoEmail != string.Empty)
            {
                _usuarioRepository.CadastraEmail(novoEmail, cpf);
                _notification.Add("OK");
            }
            else
            {
                _notification.Add("Erro");
            }
                
        }

       
    }
}