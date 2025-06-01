using System.ComponentModel.DataAnnotations;

namespace Learnly.APIs.DTOs
{
    public class EnrollmentDTO
    {
        [Required]
        public string StudentEmail { get; set; }

        [Required]
        public string CourseSelectionId { get; set; }


    }
}
