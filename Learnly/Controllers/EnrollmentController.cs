using AutoMapper;
using Learnly.APIs.DTOs;
using Learnly.APIs.Errors;
using Learnly.Core.Enrollment_Aggregate;
using Learnly.Core.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Learnly.APIs.Controllers
{
    public class EnrollmentController : BaseAPIController
    {
        private readonly IEnrollmentServicee _enrollmentService;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollmentServicee enrollmentService, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Enrollment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EnrollmentToReturn_DTO>> CreateEnrollment(EnrollmentDTO enrollmentDTO)
        {
            var enrollment = await _enrollmentService.CreateEnrollmentAsync(enrollmentDTO.StudentEmail, enrollmentDTO.CourseSelectionId);

            if (enrollment is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Enrollment, EnrollmentToReturn_DTO>(enrollment));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EnrollmentToReturn_DTO>>> GetEnrollmentsForStudent(string email)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsForStudentAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<Enrollment>, IReadOnlyList<EnrollmentToReturn_DTO>>(enrollments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentToReturn_DTO>> GetEnrollmentByIdForStudent(int id, string email) 
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdForStudentAsync(id, email);
            if (enrollment is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Enrollment, EnrollmentToReturn_DTO>(enrollment));
        }
    }
}
