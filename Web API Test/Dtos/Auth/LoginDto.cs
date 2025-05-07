using System.ComponentModel.DataAnnotations;

namespace Web_API_Test.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
