using Microsoft.AspNetCore.Mvc;
using service.Dto;
using service.interfaces;

namespace WebApplicationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassRoomController : ControllerBase 
    {
        private readonly IClassRoomService _classRoomService;

        public ClassRoomController(IClassRoomService classRoomService)
        {
            _classRoomService = classRoomService;
        }

        [HttpPost] 
        public async Task<ActionResult<bool>> CreateClass([FromBody] ClassDto classData)
        {
            if (classData == null) return BadRequest();
            var result = await _classRoomService.CreateClassAsync(classData);
            return Ok(result);
        }

        [HttpGet("{classId}")] 
        public async Task<ActionResult<ClassDetailDto>> GetClassRoomDetails(int classId)
        {
            if (classId <= 0) return BadRequest();
            var result = await _classRoomService.GetClassRoomDetailsAsync(classId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{classId}")] 
        public async Task<ActionResult<bool>> DeleteClassRoom(int classId)
        {
            if (classId <= 0) return BadRequest();
            var result = await _classRoomService.DeleteClassRoomAsync(classId);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClassDetailDto item)
        {
            if (item == null || id != item.ClassId) return BadRequest();

            var existing = await _classRoomService.GetClassRoomDetailsAsync(id);
            if (existing == null) return NotFound();

            await _classRoomService.UpdateItem(id, item);
            return NoContent();
        }

        [HttpGet] 
        public async Task<ActionResult<List<ClassDto>>> GetAllClass()
        {
            var result = await _classRoomService.getAllClassRoom();
            if (result == null) return Ok(new List<ClassDto>());
            return Ok(result);
        }
    }
}