using Microsoft.AspNetCore.Mvc;
using Repositories.models;
using service.Dto;
using service.Implementations;
using service.interfaces;
using service.services;

namespace WebApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonCategoryController : ControllerBase
    {
        private readonly ILessonCategoryService _lessonCategoryService;
        public LessonCategoryController(ILessonCategoryService lessonCategoryService)
        {
            _lessonCategoryService = lessonCategoryService;
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLessonDto classData)
        {
            var result = await _lessonCategoryService.CreateLessonCategoryAsync(classData);
            if (!result) return BadRequest("Lesson category already exists.");
            return Ok("Lesson category created successfully.");
        }
        [HttpGet("lessons-by-class")]
        public async Task<ActionResult<List<LessonCategory>>> GetLessonsByClass(string className)
        {
            var lessons = await _lessonCategoryService.GetLessonsByClassAsync(className);

            if (lessons == null || lessons.Count == 0)
                return NotFound("No lessons found for this class");

            return Ok(lessons);
        }
        [HttpDelete("{classId}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _lessonCategoryService.DeleteLessonCategory(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<CreateLessonDto> Get(int id)
        {
            var lesson = await _lessonCategoryService.GetLessonCategorysById(id);
            if (lesson == null)
                return null;
            return lesson;

        }
    }
}