using Api_intro.Data;
using Api_intro.DTOs.Sliders;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public SliderService(AppDbContext context,
                             IMapper mapper,
                             IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(SliderCreateDto sliderDto, string imageName)
        {
            var slider = _mapper.Map<Slider>(sliderDto);
            slider.Image = imageName;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await _context.Sliders.AsNoTracking()
                                                  .FirstOrDefaultAsync(x => x.Id == id)
                                                      ?? throw new NotFoundException("Slider not found!");
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<SliderDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<SliderDto>>(await _context.Sliders.AsNoTracking()
                                                                             .ToListAsync());
        }

        public async Task<SliderDto> GetByIdAsync(int id)
        {
            Slider slider = await _context.Sliders.AsNoTracking()
                                                  .FirstOrDefaultAsync(x=>x.Id == id) 
                                                      ?? throw new NotFoundException("Slider not found!");

            return _mapper.Map<SliderDto>(slider);
        }

        public async Task UpdateAsync(int id, SliderUpdateDto slider)
        {
            Slider existSlider = await _context.Sliders.AsNoTracking()
                                              .FirstOrDefaultAsync(x => x.Id == id)
                                                  ?? throw new NotFoundException("Slider not found!");

            if (slider.Image != null)
            {
                string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", existSlider.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                var uploadResult = await _fileService.UploadAsync(slider.Image);
                if (uploadResult.HasError)
                    throw new Exception(uploadResult.Response);


                _mapper.Map(slider, existSlider);
                existSlider.Image = uploadResult.Response;
            }

            

            _context.Update(existSlider);
            await _context.SaveChangesAsync();
        }

    }
}
