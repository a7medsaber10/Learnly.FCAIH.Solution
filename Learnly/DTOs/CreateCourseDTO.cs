namespace Learnly.APIs.DTOs
{
    public class CreateCourseDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int DepartmentId { get; set; }
    }
}
