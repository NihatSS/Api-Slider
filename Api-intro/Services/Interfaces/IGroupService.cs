using Api_intro.DTOs.City;
using Api_intro.DTOs.Groups;

namespace Api_intro.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task<GroupDto> GetByIdAsync(int id);
        Task CreateAsync(GroupCreateDto city);
        Task DeleteAsync(int id);
        Task EditAsync(int id, GroupEditDto city);
    }
}
