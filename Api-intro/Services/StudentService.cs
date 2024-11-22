using Api_intro.Data;
using Api_intro.DTOs.Student;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddToGroupAsync(int studentId, int groupId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId)  ?? throw new NotFoundException("Data not found!");

            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId) ?? throw new NotFoundException("Data not found!");

            student.GroupId = groupId;

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(StudentCreateDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, StudentEditDto studentDto)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id)
                ?? throw new NotFoundException("Student not found");

            _mapper.Map(studentDto, existingStudent);

            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _context.Students.Include(s => s.Group)
                                                  .AsNoTracking()
                                                  .ToListAsync();
            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _context.Students.Include(s => s.Group)
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return null;
            return _mapper.Map<StudentDto>(student);
        }
        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id) ?? throw new NotFoundException("Student not found");
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeGroupAsync(int studentId, int newGroupId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId) ?? throw new NotFoundException("Student not found");

            var newGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == newGroupId) ?? throw new NotFoundException("Group not found");

            student.GroupId = newGroupId;

            await _context.SaveChangesAsync();
        }

    }
}
