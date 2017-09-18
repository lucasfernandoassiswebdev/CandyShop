using Newtonsoft.Json;
using System.Collections.Generic;
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
        public IEnumerable<string> ContentAsString => JsonConvert.DeserializeObject<IEnumerable<string>>(Json);
    }

}
