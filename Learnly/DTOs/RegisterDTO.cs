using System.ComponentModel.DataAnnotations;

namespace Learnly.APIs.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; } = "Student";

        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "Invlid Password")]
        public string Password { get; set; }
    }
}
