using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Services.Contract
{
    public interface ICourseService
    {
        Task<Course> CreateCourseAsync(string name, string desc, string picUrl, int catId, int deptId);

        Task<Course> UpdateCourseAsync(string email);

        Task<Course> DeleteCourseAsync(string email);
    }
}
