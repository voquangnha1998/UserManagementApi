using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class SignInRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

}
