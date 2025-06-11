using AutoMapper;
using Learnly.APIs.DTOs;
using Learnly.Core.Enrollment_Aggregate;
using Learnly.Core.Entities;

namespace Learnly.APIs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForMember(c => c.Department, o => o.MapFrom(s => s.Department.Name))
                .ForMember(c => c.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(c => c.PictureUrl, o => o.MapFrom<CoursePictureUrlResolver>()).ReverseMap();

            CreateMap<CourseSelectionDTO, CourseSelection>().ReverseMap();
            CreateMap<SelectedCourseDTO, SelectedCourse>().ReverseMap();

            CreateMap<Enrollment, EnrollmentToReturn_DTO>();

            CreateMap<EnrolledCourse, EnrolledCourseDTO>()
                .ForMember(d => d.CourseUrl, o => o.MapFrom<EnrolledCoursePicUrlResolver>());

            CreateMap<CreateCourseDTO, Course>().ReverseMap();

        }
    }
}
