using System.Web.Http;

namespace WebApiSelfHostApp.Controllers
{
   public class HomeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetStatus()
        {
            return Ok("Web-Api-Self-Host Application is up and runnning on server.");
        }
    }
}
