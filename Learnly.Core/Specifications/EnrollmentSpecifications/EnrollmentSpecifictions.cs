using Learnly.Core.Enrollment_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Specifications.EnrollmentSpecifications
{
    public class EnrollmentSpecifictions : BaseSpecifications<Enrollment>
    {
        public EnrollmentSpecifictions(string studentEmail) : base(e => e.StudentEmail == studentEmail)
        {
            // eager loading => it's mandatory even if it's from type "Many"  
            Includes.Add(e => e.Courses);

            AddOrderBy(e => e.EnrollmentDate);
        }

        public EnrollmentSpecifictions(int enrollmentId, string studentEmail) : base(e => e.Id == enrollmentId && e.StudentEmail == studentEmail)
        {
            // eager loading => it's mandatory even if it's from type "Many"  
            Includes.Add(e => e.Courses);
        }


    }
}
