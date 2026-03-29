using Microsoft.AspNetCore.Mvc;
using service.Dto;
using service.interfaces;

namespace WebApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly IsubmissionService _submissionService;

        public SubmissionController(IsubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSubmissionDto>> GetSubmission(int id)
        {
            if (id <= 0) return BadRequest();

            var submission = await _submissionService.GetSubmission(id);
            if (submission == null)
            {
                return NotFound($"Submission with ID {id} not found.");
            }
            return Ok(submission);
        }

        [HttpPost("submit/{studentId}")]
        public async Task<IActionResult> SubmitAssignment(int studentId, [FromForm] CreateSubmissionDto submissionData)
        {
            if (studentId <= 0 || submissionData == null || submissionData.File == null || submissionData.File.Length == 0)
            {
                return BadRequest("Invalid data or missing file.");
            }

            var result = await _submissionService.SubmitAssignmentAsync(studentId, submissionData);
            if (result)
            {
                return Ok("Assignment submitted successfully.");
            }
            return BadRequest("Failed to submit assignment.");
        }

        [HttpPut("grade/{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] UpdateGradeDto gradeData)
        {
            if (id <= 0 || gradeData == null) return BadRequest();

            var existing = await _submissionService.GetSubmission(id);
            if (existing == null) return NotFound();

            var result = await _submissionService.UpdateSubmissionGradeAsync(id, gradeData);
            if (result)
            {
                return NoContent();
            }
            return BadRequest("Update failed.");
        }

        [HttpPut("teacher-update/{id}")]
        public async Task<IActionResult> UpdateByTeacher(int id, [FromBody] TeacherSubmissionDto item)
        {
            if (id <= 0 || item == null) return BadRequest();

            var existing = await _submissionService.GetSubmission(id);
            if (existing == null) return NotFound();

            await _submissionService.UpdateSubmissionTeacher(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmission(int id)
        {
            if (id <= 0) return BadRequest();

            var result = await _submissionService.DeleteSubmissionAsync(id);
            if (result)
            {
                return Ok("Deleted successfully.");
            }
            return NotFound();
        }
    }
}