using Api_intro.DTOs.City;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services;
using Api_intro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_intro.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cityService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city is null) return NotFound();
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityCreateDto request)
        {
            if (await _cityService.IsCountryExist(request.CountryId))
            {
                await _cityService.CreateAsync(request);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _cityService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CityEditDto request)
        {
            try
            {
                await _cityService.EditAsync(id, request);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();

            }
        }
    }
}
