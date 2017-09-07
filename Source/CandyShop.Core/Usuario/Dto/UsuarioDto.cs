using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandyShop.Core.Usuario.Dto
{
    public class UsuarioDto
    {
        public string Cpf { get; set; }
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public decimal SaldoUsuario { get; set; }
    }
}
