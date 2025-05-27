using System.ComponentModel.DataAnnotations;

namespace Learnly.APIs.DTOs
{
    public class CourseSelectionDTO
    {
        [Required]
        public string Id { get; set; } 

        public List<SelectedCourseDTO> Courses { get; set; }
    }
}
