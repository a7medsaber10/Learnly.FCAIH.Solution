using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Enrollment_Aggregate
{
    public class EnrolledCourse : BaseEntity
    {
        public EnrolledCourse(int courseId, string courseName, string courseUrl)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseUrl = courseUrl;
        }

        public EnrolledCourse()
        {
            
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseUrl { get; set; }
        public string? InstructorName { get; set; }
    }
}
