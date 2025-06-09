using System.ComponentModel.DataAnnotations;

namespace Learnly.APIs.DTOs
{
    public class ForgetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
