using System.ComponentModel.DataAnnotations;

namespace Learnly.APIs.DTOs
{
    public class SelectedCourseDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Category { get; set; }
    }
}