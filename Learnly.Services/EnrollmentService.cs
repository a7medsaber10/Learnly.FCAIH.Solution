using Learnly.Core.Enrollment_Aggregate;
using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
using Learnly.Core.Services.Contract;
using Learnly.Core.Specifications;
using Learnly.Core.Specifications.EnrollmentSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Services
{
    public class EnrollmentService : IEnrollmentServicee
    {
        private readonly ICourseSelectionRepository _selectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(ICourseSelectionRepository selectionRepository, IUnitOfWork unitOfWork)
        {
            _selectionRepository = selectionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Enrollment?> CreateEnrollmentAsync(string studentEmail, string courseSelectionId)
        {
            var courseSelection = await _selectionRepository.GetCourseSelectionAsync(courseSelectionId);

            var enrolledCourses = new List<EnrolledCourse>();

            if (courseSelection?.Courses?.Count() > 0) 
            {
                foreach (var course in courseSelection.Courses)
                {
                    var myCourse = await _unitOfWork.Repository<Course>().GetAsync(course.Id);
                    var enrolledCourse = new EnrolledCourse(course.Id,myCourse.Name, myCourse.PictureUrl);

                    enrolledCourses.Add(enrolledCourse);
                }
            }

            var enrollment = new Enrollment(studentEmail, enrolledCourses);

            await _unitOfWork.Repository<Enrollment>().AddAsync(enrollment);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)  return null;
            return enrollment;
        }

        public async Task<Enrollment> GetEnrollmentByIdForStudentAsync(int enrollmentId, string studentEmail)
        {
            var enrollmentRepository = _unitOfWork.Repository<Enrollment>();

            var specifications = new EnrollmentSpecifictions(enrollmentId, studentEmail);

            var enrollment = await enrollmentRepository.GetWithSpecAsync(specifications);

            return enrollment;
        }

        public async Task<IReadOnlyList<Enrollment>> GetEnrollmentsForStudentAsync(string studentEmail)
        {
            var enrollmentRepository = _unitOfWork.Repository<Enrollment>();

            var specifications = new EnrollmentSpecifictions(studentEmail);

            var enrollments = await enrollmentRepository.GetAllWithSpecAsync(specifications);

            return enrollments;
        }
    }
}
