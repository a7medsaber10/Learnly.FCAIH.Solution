using AutoMapper;
using AutoMapper.Execution;
using Learnly.APIs.DTOs;
using Learnly.Core.Entities;

namespace Learnly.APIs.Helpers
{
    public class CoursePictureUrlResolver : IValueResolver<Course, CourseDTO, string>
    {
        private readonly IConfiguration _configuration;

        public CoursePictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Course source, CourseDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
