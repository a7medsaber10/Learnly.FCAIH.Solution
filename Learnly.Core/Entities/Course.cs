using Learnly.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Entities
{
    public class Course : BaseEntity
    {
        public Course()
        {

        }

        public Course(string name, string desc, string picUrl, int catId,int deptId)
        {
            Name = name;
            Description = desc;
            PictureUrl = picUrl;
            CategoryId = catId;
            DepartmentId = deptId;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }
        public CourseCategory Category { get; set; }

        public int DepartmentId { get; set; }
        public CourseDepartment Department { get; set; }

        //public string StudentId { get; set; }

        //[ForeignKey("StudentId")]
        //public AppUser Student { get; set; }

        //public string TeacherId { get; set; }

        //[ForeignKey("TeacherId")]
        //public Teacher Teacher { get; set; }
    }
}
