using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Specifications.CourseSpecifications
{
    public class CourseWithDeptartmentAndCategorySpec : BaseSpecifications<Course>
    {
        public CourseWithDeptartmentAndCategorySpec(CourseSpecificationParameters parameters) 
            : base(c => 
            // Searching by name 
            (string.IsNullOrEmpty(parameters.Search) || c.Name.ToLower().Contains(parameters.Search.ToLower()))
            &&
            // Criteria
            (!parameters.CategoryId.HasValue || c.CategoryId == parameters.CategoryId.Value) 
            && 
            (!parameters.DepartmentId.HasValue || c.DepartmentId == parameters.DepartmentId.Value))
        {
            Includes.Add(c => c.Department);
            Includes.Add(c => c.Category);
            //Includes.Add(c => c.Student);
            //Includes.Add(c=>c.Teacher);

            if(!string.IsNullOrEmpty(parameters.Sort))
            {
                switch (parameters.Sort)
                {
                    case "Name":
                        AddOrderBy(c => c.Name);
                        break;
                    case "NameDesc":
                        AddOrderByDescending(c => c.Name);
                        break;
                    default:
                        AddOrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                AddOrderBy(c => c.Id);
            }

            ApplyPagination((parameters.PageIndex - 1) * parameters.PageSize, parameters.PageSize);
        }

        public CourseWithDeptartmentAndCategorySpec(int id) : base(c => c.Id == id)
        {
            Includes.Add(c => c.Department);
            Includes.Add(c => c.Category);
        }
    }
}
