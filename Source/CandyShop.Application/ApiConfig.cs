using System.Configuration;

namespace CandyShop.Application
{
    public class ApiConfig
    {
        public static string enderecoApi { get; set; } = ConfigurationManager.AppSettings["IP_API"];
    }
}
