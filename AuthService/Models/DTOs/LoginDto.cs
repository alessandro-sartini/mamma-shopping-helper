using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email o Username obbligatorio")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password obbligatoria")]
        public string Password { get; set; } = string.Empty;
    }
}
