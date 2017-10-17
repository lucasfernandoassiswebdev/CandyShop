using System.Configuration;

namespace CandyShop.WebAPI
{
    public class ImagensConfig
    {
        public static string enderecoImagens { get; set; } = ConfigurationManager.AppSettings["IP_Imagens"];
        public static string getEnderecoImagens { get; set; } = ConfigurationManager.AppSettings["IP_GetImagens"];
    }
}