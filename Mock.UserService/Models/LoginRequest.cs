using System.ComponentModel.DataAnnotations;

namespace Mock.UserService.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

