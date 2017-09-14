using System;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class PingController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new
            {
                Id = 1,
                Nome = "Teste",
                Ativo = true,
                DataNascimento = DateTime.Now
            });
        }
    }
}
