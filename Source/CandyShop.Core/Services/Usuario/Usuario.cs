namespace CandyShop.Core.Services.Usuario
{
    public class Usuario
    {
        public string Cpf { get; set; }
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public decimal SaldoUsuario { get; set; }
        public string Ativo { get; set; }
        public string Classificacao { get; set; }
        public string Imagem { get; set; }
        public bool RemoverImagem { get; set; }
        public string FirstLogin { get; set; }
        public string Email { get; set; }

        /* As linhas de codigo abaixo é onde ficam todas as verificações de usuario como 
            validar cpf, senha, tamanho dos campos  etc */
        public bool VerificaInsercao(INotification notification)
        {
            if (!ValidaCpf(Cpf))
                notification.Add("Cpf Invalido");
            if (string.IsNullOrEmpty(NomeUsuario.Trim()) || NomeUsuario.Length > 50)
                notification.Add("Nome do Usuario invalido ");
            return !notification.HasNotification();
        }

        public bool VerificaEdicao(INotification notification)
        {
            if (string.IsNullOrEmpty(NomeUsuario.Trim()) || NomeUsuario.Length > 50)
                notification.Add("Nome do Usuario invalido ");
            if (string.IsNullOrEmpty(SenhaUsuario.Trim()) || SenhaUsuario.Length > 12 || SenhaUsuario.Length < 6)
                notification.Add("Senha invalida");
            if (string.IsNullOrEmpty(Ativo))
                notification.Add("Status do usuario nao pode ser nulo");
            if (string.IsNullOrEmpty(Classificacao.Trim()) || Classificacao.Length > 1)
                notification.Add("Classificacao irregular");

            return !notification.HasNotification();
        }

        //Função de calculo para verificar se o cpf é valido
        private static bool ValidaCpf(string cpf)
        {
            var mt1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var mt2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" ||
                cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" ||
                cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" ||
                cpf == "99999999999")
                return false;

            if (cpf.Length != 11)
                return false;

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt1[i];

            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cpf.EndsWith(digito);
        }
    }
}
