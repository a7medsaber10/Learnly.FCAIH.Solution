using System.ComponentModel.DataAnnotations;

namespace Learnly.APIs.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
