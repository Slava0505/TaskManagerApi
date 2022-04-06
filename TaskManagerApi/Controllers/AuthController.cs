using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // POST api/<AuthController>
        [HttpPost]
        [Route("api/[controller]/login")]

        public void PostLogin([FromBody] string value)
        {
        }
        // POST api/<AuthController>
        [HttpPost]
        [Route("api/[controller]/logout")]

        public void PostLogout([FromBody] string value)
        {
        }
        // POST api/<AuthController>
        [HttpPost]
        [Route("api/[controller]/register")]

        public void PostRegister([FromBody] string value)
        {
        }
    }
}
