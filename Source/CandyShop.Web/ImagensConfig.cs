using System.Configuration;

namespace CandyShop.Web
{
    public class ImagensConfig
    {
        public static string EnderecoImagens { get; set; } = ConfigurationManager.AppSettings["IP_GetImagens"];
    }
}