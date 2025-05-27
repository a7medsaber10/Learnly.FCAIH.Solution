using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Specifications.CourseSpecifications
{
    public class CourseSpecificationParameters
    {
        public string? Sort { get; set; }

        public int? DepartmentId { get; set; }

        public int? CategoryId { get; set; }


        public const int MaxPageSize = 10;
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        public string? Search { get; set;}

    }
}
