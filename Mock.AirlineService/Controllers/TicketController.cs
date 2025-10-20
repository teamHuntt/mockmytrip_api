using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mock.AirlineService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpGet("book")]
        public Task<IActionResult> BookTicket()
        {
            return Task.FromResult<IActionResult>(Ok(new { message = "Ticket booked successfully", isSuccess = true }));
        }
    }
}
