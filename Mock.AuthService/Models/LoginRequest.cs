using System.ComponentModel.DataAnnotations;

namespace Mock.AuthService.Models
{
    public class LoginRequest
    {
        public required string UserId { get; set; }
        public required string Password { get; set; }
    }
}

