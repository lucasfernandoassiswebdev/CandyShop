using System;
using System.Web.Http;

namespace CandyShop.WebAPI.Controllers
{
    public class PingController : ApiController
    {
        [HttpGet,Route("api")]
        public IHttpActionResult Get()
        {
            return Ok("Status: OK; Data: " + DateTime.Now);
        }
    }
}
