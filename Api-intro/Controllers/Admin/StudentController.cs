using Api_intro.Data;
using Api_intro.DTOs.Student;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_intro.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student is null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto request)
        {
            if (!ModelState.IsValid || request.Age == 0 || request.GroupId == 0) return BadRequest();
            await _studentService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = request.GroupId }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] StudentEditDto request)
        {
            try
            {
                await _studentService.EditAsync(id, request);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _studentService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/{groupId}")]
        public async Task<IActionResult> ChangeGroup([FromRoute] int id, [FromRoute] int groupId)
        {
            try
            {
                await _studentService.ChangeGroupAsync(id, groupId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/{groupId}")]
        public async Task<IActionResult> AddToGroup([FromRoute] int id, [FromRoute] int groupId)
        {
            try
            {
                await _studentService.AddToGroupAsync(id, groupId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
