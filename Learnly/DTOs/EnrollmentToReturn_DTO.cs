using Learnly.Core.Enrollment_Aggregate;

namespace Learnly.APIs.DTOs
{
    public class EnrollmentToReturn_DTO
    {
        public int Id { get; set; }
        public string StudentEmail { get; set; }
        public DateTimeOffset EnrollmentDate { get; set; }
        public string Status { get; set; }
        public ICollection<EnrolledCourseDTO> Courses { get; set; } = new HashSet<EnrolledCourseDTO>();
    }
}
