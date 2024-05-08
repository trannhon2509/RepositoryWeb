using Microsoft.AspNetCore.Mvc;
using RevibeCO.Models.Request;

namespace RevibeCO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public AuthController()
        {

        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            return Ok("Login success");
        }

    }
}
