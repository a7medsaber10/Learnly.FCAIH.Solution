using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Entities
{
    public class CourseDepartment : BaseEntity
    {
        public string Name { get; set; }

        //public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
