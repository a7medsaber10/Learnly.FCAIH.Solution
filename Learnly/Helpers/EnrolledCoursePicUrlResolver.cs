using AutoMapper;
using AutoMapper.Execution;
using Learnly.APIs.DTOs;
using Learnly.Core.Enrollment_Aggregate;

namespace Learnly.APIs.Helpers
{
    public class EnrolledCoursePicUrlResolver : IValueResolver<EnrolledCourse, EnrolledCourseDTO, string>
    {
        private readonly IConfiguration _configuration;

        public EnrolledCoursePicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(EnrolledCourse source, EnrolledCourseDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.CourseUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.CourseUrl}";
            }
            return string.Empty;
        }
    }
}
