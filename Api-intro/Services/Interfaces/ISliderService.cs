using Api_intro.DTOs.Sliders;
using Api_intro.Models;

namespace Api_intro.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderDto>> GetAllAsync();
        Task<SliderDto> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateDto sliderDto, string imageName);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, SliderUpdateDto slider);
    }
}
