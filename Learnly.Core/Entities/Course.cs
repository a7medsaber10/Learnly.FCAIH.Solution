using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public int CategoryId { get; set; }
        public CourseCategory Category { get; set; }

        public int DepartmentId { get; set; }
        public CourseDepartment Department { get; set; }
    }
}
