using Learnly.Core.Enrollment_Aggregate;
using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
using Learnly.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Course> CreateCourseAsync(string name, string desc, string picUrl, int catId, int deptId)
        {
            var course = new Course(name, desc, picUrl, catId, deptId);

            await _unitOfWork.Repository<Course>().AddAsync(course);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;
            return course;
        }

        public Task<Course> DeleteCourseAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Course> UpdateCourseAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
