using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Entities
{
    public class CourseSelection
    {
        public CourseSelection(string id)
        {
            Id = id;
            Courses = new List<SelectedCourse>();
        }
        public string Id { get; set; } // front send the id => Guid, So id must be string 

        public List<SelectedCourse> Courses { get; set; }
    }
}
