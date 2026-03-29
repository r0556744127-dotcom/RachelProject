using Microsoft.AspNetCore.Mvc;
using Repositories.models;
using Repositories.Repositories;
using service.Dto;
using service.interfaces;
using service.services;

namespace WebApplicationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateStaffMember([FromBody] CreateStaffDto staffData)
        {
            if (staffData == null) return BadRequest();
            var result = await _staffService.CreateStaffMemberAsync(staffData);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<CreateStaffDto>>> GetAll()
        {
            var result = await _staffService.GetAllStaff();
            if (result == null) return Ok(new List<CreateStaffDto>());
            return Ok(result);
        }
        [HttpGet("progress/{staffId}")]
        public async Task<ActionResult<StudentDto>> GetStudentProgress(int studentId)
        {
            if (studentId <= 0) return BadRequest();
            var result = await _staffService.GetStudentProgressAsync(studentId);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStuff(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await _staffService.GetStuffById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] string newPassword)
        {
            var success = await _staffService.UpdateInitialPasswordAsync(id, newPassword);

            if (!success)
            {
                return BadRequest("עדכון הסיסמה נכשל. וודא שהסטודנט קיים במערכת.");
            }

            return Ok("הסיסמה עודכנה בהצלחה.");
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login([FromBody] LoginUser loginUser)
        {
            var response = await _staffService.GetStaffLoginAsync(loginUser);

            if (response == null)
            {
                // מחזירים 401 אם השם משתמש או הסיסמה לא נכונים
                return Unauthorized("מספר זהות או סיסמה שגויים.");
            }

            return Ok(response);
        }
    }
}