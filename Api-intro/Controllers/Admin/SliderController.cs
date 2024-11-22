using Api_intro.DTOs.Sliders;
using Api_intro.Helpers.Exceptions;
using Api_intro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_intro.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ISliderService _sliderService;
        public SliderController(IFileService fileService, 
                                ISliderService sliderService)
        {
            _fileService = fileService;
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sliderService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _sliderService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            var response = await _fileService.UploadAsync(file);
            if(response.HasError)
                return BadRequest(response.Response);
            return Ok(response.Response);
        }

        [HttpDelete]
        public IActionResult DeleteFile(string fileName)
        {
            _fileService.Delete(fileName);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
        {
            var response = await _fileService.UploadAsync(request.Image);
            if (response.HasError)
                return BadRequest(response.Response);

            await _sliderService.CreateAsync(request, response.Response);

            return CreatedAtAction(nameof(Create), "Slider successfully created");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await _sliderService.DeleteAsync(id);
                return Ok("Slider successfully deleted!");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] SliderUpdateDto request)
        {
            try
            {
                await _sliderService.UpdateAsync(id, request);
                return Ok("Slider successfully updated!");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
