using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
