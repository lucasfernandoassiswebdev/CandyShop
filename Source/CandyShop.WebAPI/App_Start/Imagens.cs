using System;
using System.Drawing;
using System.IO;

namespace CandyShop.WebAPI
{
    public class Imagens
    {
        private static readonly string EnderecoImagens = $"{ImagensConfig.EnderecoImagens}";

        public void InserirImagem(string imagem, string nomeImagem)
        {
            string[] prefixos = { "data:image/jpeg;base64,", "data:image/png;base64,", "data:image/jpg;base64," };
            foreach (var prefixo in prefixos)
            {
                if (!imagem.StartsWith(prefixo)) continue;
                imagem = imagem.Substring(prefixo.Length);
                var bytes = Convert.FromBase64String(imagem);
                var caminho = $"{nomeImagem}.jpg";
                if (File.Exists(caminho))
                    File.Delete(caminho);
                File.WriteAllBytes(caminho, bytes);
            }
        }

        public void RemoverImagem(string filePath)
        {
            var caminho = $"{EnderecoImagens}\\{filePath}.jpg";
            if (File.Exists(caminho))
            {
                File.Delete(caminho);
                InserirPadrao(filePath);
            }
            InserirPadrao(filePath);
        }

        public void InserirPadrao(string endereco)
        {
            //pegando a imagem na aplicação e transformando em base 64
            var imagem = ConvertTo64();
            //transformando em array de bytes e salvando com o cpf do usuário
            var bytes = Convert.FromBase64String(imagem);
            endereco = $"{endereco}.jpg";
            File.WriteAllBytes(endereco, bytes);
        }

        private static string ConvertTo64()
        {
            using (var image = Image.FromFile($"{EnderecoImagens}/sem-foto.png"))
            {
                using (var m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    var imageBytes = m.ToArray();
                    // Convert byte[] to Base64 String
                    var base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}