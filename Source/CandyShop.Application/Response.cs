using Newtonsoft.Json;
using System.Net;

namespace CandyShop.Application
{
    public class Response<T>
    {
        public Response(string json, HttpStatusCode statusCode)
        {
            Json = json;
            Status = statusCode;
        }

        public Response(HttpStatusCode statusCode)
        {
            Status = statusCode;
        }

        private string Json { get; }
        public HttpStatusCode Status { get; }

        public T Content => JsonConvert.DeserializeObject<T>(Json);
        public string ContentAsString => JsonConvert.DeserializeObject<string>(Json);
    }

}
