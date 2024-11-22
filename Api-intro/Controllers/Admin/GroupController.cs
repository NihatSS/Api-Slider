using Api_intro.DTOs.Groups;
using Api_intro.Helpers.Exceptions;
using Api_intro.Services;
using Api_intro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_intro.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAllAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group is null) return NotFound();
            return Ok(group);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto request)
        {
            if (!ModelState.IsValid || request.Capacity == 0) return BadRequest("Field can't be null");
            await _groupService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Successfully created");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _groupService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] GroupEditDto request)
        {
            try
            {
                await _groupService.EditAsync(id, request);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
