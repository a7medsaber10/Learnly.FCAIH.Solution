using AutoMapper;
using Learnly.APIs.DTOs;
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
                .ForMember(c => c.PictureUrl, o => o.MapFrom<CoursePictureUrlResolver>());

            CreateMap<CourseSelectionDTO, CourseSelection>().ReverseMap();
            CreateMap<SelectedCourseDTO, SelectedCourse>().ReverseMap();
        }
    }
}
