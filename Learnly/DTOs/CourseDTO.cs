namespace Learnly.APIs.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public int DepartmentId { get; set; }
        public string Department { get; set; }
    }
}
