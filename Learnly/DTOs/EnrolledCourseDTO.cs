namespace Learnly.APIs.DTOs
{
    public class EnrolledCourseDTO
    {

        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseUrl { get; set; }
        public string? InstructorName { get; set; }
    }
}
