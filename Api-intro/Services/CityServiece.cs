using Api_intro.Data;
using Api_intro.DTOs.City;
using Api_intro.DTOs.Countries;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Api_intro.Services
{
    public class CityServiece : ICityService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CityServiece(AppDbContext context,
                            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CityCreateDto city)
        {
            await _context.Cities.AddAsync(_mapper.Map<City>(city));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id) ?? throw new NotFoundException("Data notfound");
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, CityEditDto city)
        {
            var existCity = await _context.Cities.AsNoTracking()
                                                 .FirstOrDefaultAsync(m => m.Id == id) ?? throw new NotFoundException("Data notfound");

            _mapper.Map(city, existCity);


            _context.Cities.Update(existCity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            return _mapper.Map<List<CityDto>>(await _context.Cities.AsNoTracking()
                                                                   .ToListAsync());
        }

        public async Task<CityDto> GetByIdAsync(int id)
        {
            var result = await _context.Cities.AsNoTracking()
                                              .FirstOrDefaultAsync(m => m.Id == id);

            if (result is null) return null;

            return _mapper.Map<CityDto>(result);
        }

        public async Task<bool> IsCountryExist(int countryId)
        {
            if(await _context.Countries.AsNoTracking()
                                       .AnyAsync(x => x.Id == countryId)) return true;
            
            return false;
        }
    }
}
