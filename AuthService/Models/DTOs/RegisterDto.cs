using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username obbligatorio")]
        [MaxLength(50, ErrorMessage = "Username massimo 50 caratteri")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email obbligatoria")]
        [EmailAddress(ErrorMessage = "Email non valida")]
        [MaxLength(100, ErrorMessage = "Email massimo 100 caratteri")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password obbligatoria")]
        [MinLength(6, ErrorMessage = "Password minimo 6 caratteri")]
        public string Password { get; set; } = string.Empty;
    }
}
