using AutoMapper;
using Learnly.APIs.DTOs;
using Learnly.APIs.Errors;
using Learnly.APIs.Helpers;
using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
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

        public CoursesController(
                IGenericRepository<Course> courseRepository, 
                IMapper mapper,
                IGenericRepository<CourseCategory> categoryRepository,
                IGenericRepository<CourseDepartment> departmentRepository
            )
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _departmentRepository = departmentRepository;
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
