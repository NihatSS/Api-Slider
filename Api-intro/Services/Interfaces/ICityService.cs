using Api_intro.DTOs.City;

namespace Api_intro.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<CityDto> GetByIdAsync(int id);
        Task CreateAsync(CityCreateDto city);
        Task DeleteAsync(int id);
        Task EditAsync(int id, CityEditDto city);
        Task<bool> IsCountryExist(int countryId);

    }
}
