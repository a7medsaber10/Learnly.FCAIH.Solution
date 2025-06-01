using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Enrollment_Aggregate
{
    public class Enrollment : BaseEntity
    {
        public Enrollment(string studentEmail, ICollection<EnrolledCourse> courses)
        {
            StudentEmail = studentEmail;
            Courses = courses;
        }

        public Enrollment()
        {
            
        }
        public string StudentEmail { get; set; }
        public DateTimeOffset EnrollmentDate { get; set; } = DateTimeOffset.UtcNow;
        public EnrollmentStatus Status { get; set; }
        public ICollection<EnrolledCourse> Courses { get; set; } = new HashSet<EnrolledCourse>();
    }
}
