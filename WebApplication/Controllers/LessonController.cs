using Microsoft.AspNetCore.Mvc;
using Repositories.models;
using service.Dto;
using service.interfaces;
using service.services;

namespace WebApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        // POST: api/Lesson
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LessonDto lessonData)
        {
            var result = await _lessonService.CreateLesson(lessonData);
            if (!result)
                return BadRequest("Lesson already exists.");

            return Ok("Lesson created successfully.");
        }

        // GET: api/Lesson/lessons-by-categoryLesson?categoryId=1
        [HttpGet("lessons-by-categoryLesson")]
        public async Task<ActionResult<List<LessonDto>>> GetLessonsByLessonCategory([FromQuery] int categoryId)
        {
            var lessons = await _lessonService.GetLessonsByLessonCategory(categoryId);

            if (lessons == null || lessons.Count == 0)
                return NotFound("No lessons found for this category.");

            return Ok(lessons);
        }

        // DELETE: api/Lesson/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _lessonService.DeleteLesson(id);
            if (!result)
                return NotFound("Lesson not found.");

            return Ok("Lesson deleted successfully.");
        }

        // GET: api/Lesson/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> Get(int id)
        {
            var lesson = await _lessonService.GetLessonById(id);
            if (lesson == null)
                return NotFound("Lesson not found.");

            return Ok(lesson);
        }
    }
}