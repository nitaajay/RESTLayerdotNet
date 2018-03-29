using System.Linq;
using System.Web.Http;
using WebApiSelfHostApp.BusinessLayer;
using WebApiSelfHostApp.Models;

namespace WebApiSelfHostApp.Controllers
{
    public class UserMasterController : ApiController
    {
        private readonly UserMasterHelper _userMasterHelper = new UserMasterHelper();

        public IHttpActionResult Post([FromBody] UserMaster userMaster)
        {
            if (!ModelState.IsValid) return BadRequest("Model is not valid");

            var userDatails = _userMasterHelper.GetUserMaster(userMaster: userMaster);
            if (userDatails.Any()) return Ok(userDatails.First() /* userDatails */);

            return BadRequest("User not found");
        }
    }
}
