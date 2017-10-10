using System.Configuration;

namespace CandyShop.Application
{
    /* Como a camada de aplicação é a responsável por se comunicar com a API, aqui 
      é passado o definido o endereço dessa API, que será usado pelas classes de aplicação */
    public class ApiConfig
    {
        public static string enderecoApi { get; set; } = ConfigurationManager.AppSettings["IP_API"];
    }
}
