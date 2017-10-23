using System.Configuration;

namespace CandyShop.WebAPI
{
    public class ImagensConfig
    {
        public static string EnderecoImagens { get; set; } = ConfigurationManager.AppSettings["IP_Imagens"];
        public static string GetEnderecoImagens { get; set; } = ConfigurationManager.AppSettings["IP_GetImagens"];
    }
}