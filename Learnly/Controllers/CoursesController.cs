using AutoMapper;
using Learnly.APIs.DTOs;
using Learnly.APIs.Errors;
using Learnly.APIs.Helpers;
using Learnly.Core.Enrollment_Aggregate;
using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
using Learnly.Core.Services.Contract;
using Learnly.Core.Specifications;
using Learnly.Core.Specifications.CourseSpecifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learnly.APIs.Controllers
{
    public class CoursesController : BaseAPIController
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CourseCategory> _categoryRepository;
        private readonly IGenericRepository<CourseDepartment> _departmentRepository;
        private readonly ICourseService _courseService;

        public CoursesController
        (
                IGenericRepository<Course> courseRepository, 
                IMapper mapper,
                IGenericRepository<CourseCategory> categoryRepository,
                IGenericRepository<CourseDepartment> departmentRepository,
                ICourseService courseService
        )
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _departmentRepository = departmentRepository;
            _courseService = courseService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IReadOnlyList<CourseDTO>>> GetCourses([FromQuery]CourseSpecificationParameters parameters)
        {
            var spec = new CourseWithDeptartmentAndCategorySpec(parameters);
            var courses = await _courseRepository.GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseDTO>>(courses);

            var countSpec = new CourseWithFiltrationForCountSpec(parameters);

            var count = await _courseRepository.GetCountAsync(countSpec);

            return Ok(new Pagination<CourseDTO>(parameters.PageSize, parameters.PageIndex, count, data));
        }

        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourseById(int id)
        {
            var spec = new CourseWithDeptartmentAndCategorySpec(id);
            var course = await _courseRepository.GetWithSpecAsync(spec);

            if(course == null)
            {
                return NotFound(new ApiResponse(404));
            }
            else
            {
                return Ok(_mapper.Map<Course, CourseDTO>(course));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Teacher")]
        [HttpPost("create-course")]
        public async Task<ActionResult<CreateCourseDTO>> CreateCourse(CourseDTO courseDTO)
        {
            var newCourse = await _courseService.CreateCourseAsync
                (
                    courseDTO.Name,
                    courseDTO.Description,
                    courseDTO.PictureUrl,
                    courseDTO.CategoryId,
                    courseDTO.DepartmentId
                );

            if (newCourse is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Course, CreateCourseDTO>(newCourse));
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Teacher")]
        [HttpPut("update-course/{id}")]
        public async Task<ActionResult<CreateCourseDTO>> UpdateCourse(int id,CourseDTO courseDTO)
        {
            var spec = new CourseWithDeptartmentAndCategorySpec(id);
            var myCourse = await _courseRepository.GetWithSpecAsync(spec);

            if (myCourse == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedCourse = _mapper.Map(courseDTO, myCourse);

            var course = await _courseService.UpdateCourseAsync(mappedCourse);

            if (course == null)  return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Course, CreateCourseDTO>(course));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Teacher")]
        [HttpDelete("delete-course/{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var spec = new CourseWithDeptartmentAndCategorySpec(id);
            var myCourse = await _courseRepository.GetWithSpecAsync(spec);

            if (myCourse == null)
            {
                return NotFound(new ApiResponse(404));
            }

            await _courseService.DeleteCourseAsync(myCourse);

            return Ok("Course Deleted Successfully.");
        }


        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<CourseDepartment>>> GetDepartments()
        {
            var departments = await _departmentRepository.GetAllAsync();

            return Ok(departments);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CourseCategory>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return Ok(categories);
        }
    }
}
