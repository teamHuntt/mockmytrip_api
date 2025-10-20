using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mock.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpGet("refereshtoken")]
        public Task<IActionResult> GetToken()
        {
            // 📝 Mock token generation
            var token = Guid.NewGuid().ToString();
            return Task.FromResult<IActionResult>(Ok(new { token }));
        }
    }
}
