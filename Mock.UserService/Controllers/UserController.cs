using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mock.UserService.Models;

namespace Mock.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 📝 Mock validation
            if (request.UserId == "admin" && request.Password == "password123")
            {
                return Ok( new {message = "Valid User Success",isSuccess = true });
            }
            else
            {
                return Unauthorized("Unauthorized");
            }
        }
    }
}
