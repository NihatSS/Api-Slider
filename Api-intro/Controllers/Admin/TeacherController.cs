using Api_intro.DTOs.Teachers;
using Api_intro.Helpers.Exceptions;
using Api_intro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_intro.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            return Ok(teachers);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                TeacherDto teacher = await _teacherService.GetByIdAsync(id);
                return Ok(teacher);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto teacher)
        {
            if (teacher == null || teacher.Age == 0 || teacher.GroupId == 0) return BadRequest();
            await _teacherService.CreateAsync(teacher);
            return CreatedAtAction(nameof(Create), "Successfully created!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _teacherService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TeacherEditDto teacher)
        {
            try
            {
                await _teacherService.UpdateAsync(id, teacher);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{teacherId}/{groupId}")]
        public async Task<IActionResult> ChangeTeacherGroup([FromRoute] int teacherId, [FromRoute] int groupId)
        {
            try
            {
                await _teacherService.ChangeTeacherGroupAsync(teacherId, groupId);
                return Ok("TEacher's group successfully changed");
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (AlreadyHasException)
            {
                return BadRequest();
            }
        }

        [HttpPut("{teacherId}/{groupId}")]
        public async Task<IActionResult> AddTeacherToGroup(int teacherId, int groupId)
        {
            try
            {
                await _teacherService.AddTeacherToGroupAsync(teacherId, groupId);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (AlreadyHasException)
            {
                return BadRequest();
            }
        }
    }
}
