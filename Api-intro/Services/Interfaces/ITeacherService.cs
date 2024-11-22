using Api_intro.DTOs.Teachers;

namespace Api_intro.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(int id);
        Task CreateAsync(TeacherCreateDto teacher);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, TeacherEditDto teacher);
        Task AddTeacherToGroupAsync(int teacherId, int groupId);
        Task ChangeTeacherGroupAsync(int teacherId, int newGroupId);
    }
}
