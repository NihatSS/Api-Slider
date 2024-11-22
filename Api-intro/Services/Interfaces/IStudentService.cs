using Api_intro.DTOs.Student;
using Api_intro.Models;

namespace Api_intro.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task CreateAsync(StudentCreateDto studentDto);
        Task EditAsync(int id, StudentEditDto studentDto);
        Task DeleteAsync(int id);

        Task ChangeGroupAsync(int studentId, int newGroupId);
        Task AddToGroupAsync(int studentId, int groupId);
    }
}
