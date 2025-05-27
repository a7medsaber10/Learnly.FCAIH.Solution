using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Specifications.CourseSpecifications
{
    public class CourseWithFiltrationForCountSpec : BaseSpecifications<Course>
    {
        public CourseWithFiltrationForCountSpec(CourseSpecificationParameters parameters) 
            : base(c =>
            (string.IsNullOrEmpty(parameters.Search) || c.Name.ToLower().Contains(parameters.Search.ToLower()))
            &&
            (!parameters.CategoryId.HasValue || c.CategoryId == parameters.CategoryId.Value) 
            && 
            (!parameters.DepartmentId.HasValue || c.DepartmentId == parameters.DepartmentId.Value))
        {
            
        }
    }
}
