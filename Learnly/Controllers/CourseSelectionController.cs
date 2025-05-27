using AutoMapper;
using Learnly.APIs.DTOs;
using Learnly.APIs.Errors;
using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learnly.APIs.Controllers
{

    public class CourseSelectionController : BaseAPIController
    {
        private readonly ICourseSelectionRepository _courseSelectionRepository;
        private readonly IMapper _mapper;

        public CourseSelectionController(ICourseSelectionRepository courseSelectionRepository, IMapper mapper)
        {
            _courseSelectionRepository = courseSelectionRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseSelection>> GetCourseSelection(string id)
        {
            var selection = await _courseSelectionRepository.GetCourseSelectionAsync(id);

            return Ok(selection ?? new CourseSelection(id));
        }

        [HttpPost]
        public async Task<ActionResult<CourseSelection>> UpdateCourseSelection(CourseSelectionDTO selection)
        {
            // mapping from CourseSelectionDTO => CourseSelection
            var mappedSelection = _mapper.Map<CourseSelectionDTO, CourseSelection>(selection);

            var createOrUpdateSelection = await _courseSelectionRepository.UpdateCourseSelectionAsync(mappedSelection);

            if(createOrUpdateSelection is null) return BadRequest(new ApiResponse(400));

            return Ok(createOrUpdateSelection);
        }

        [HttpDelete]
        public async Task DeleteCourseSelection(string id)
        {
            await _courseSelectionRepository.DeleteCourseSelectionAsync(id);
        }
    }
}
