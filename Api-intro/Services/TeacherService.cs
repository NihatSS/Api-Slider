using Api_intro.Data;
using Api_intro.DTOs.Teachers;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TeacherService(AppDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddTeacherToGroupAsync(int teacherId, int groupId)
        {
            Teacher teacher = await _context.Teachers.FindAsync(teacherId)
                ?? throw new NotFoundException("Teacher not found!");


            Group group = await _context.Groups.FindAsync(groupId)
                ?? throw new NotFoundException("Group not found!");

            bool isAlreadyAssigned = await _context.TeacherGroups.AnyAsync(x => x.TeacherId == teacherId && x.GroupId == groupId);
            if (isAlreadyAssigned)
            {
                throw new AlreadyHasException();
            }

            var teacherGroup = new TeacherGroup
            {
                TeacherId = teacherId,
                GroupId = groupId
            };

            await _context.TeacherGroups.AddAsync(teacherGroup);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeTeacherGroupAsync(int teacherId, int newGroupId)
        {
            TeacherGroup teacherGroup = await _context.TeacherGroups.FirstOrDefaultAsync(x => x.TeacherId == teacherId)
                                                                         ?? throw new NotFoundException("Teacher is not assigned to any group!");

            Group newGroup = await _context.Groups.FindAsync(newGroupId)
                                                      ?? throw new NotFoundException("New group not found!");

            teacherGroup.GroupId = newGroupId;

            _context.TeacherGroups.Update(teacherGroup);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(TeacherCreateDto teacherDto)
        {
            var teacher = _mapper.Map<Teacher>(teacherDto);

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == teacherDto.GroupId);
            if (group == null)
            {
                throw new NotFoundException("Group not found!");
            }

            var teacherGroup = new TeacherGroup
            {
                TeacherId = teacher.Id,
                GroupId = teacherDto.GroupId
            };

            await _context.TeacherGroups.AddAsync(teacherGroup);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(x=>x.Id == id) 
                                                        ?? throw new NotFoundException("Data not found!");
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _context.Teachers.AsNoTracking()
                                                               .ToListAsync());
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            Teacher teacher = await _context.Teachers.AsNoTracking()
                                                     .FirstOrDefaultAsync(x=>x.Id == id);
            if (teacher == null) throw new NotFoundException("Data not found!");
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task UpdateAsync(int id, TeacherEditDto teacher)
        {
            var existTeacher = await _context.Teachers.AsNoTracking()
                                                 .FirstOrDefaultAsync(x => x.Id == id)
                                                    ?? throw new NotFoundException("Data not found");
            _mapper.Map(teacher, existTeacher);
            _context.Update(existTeacher);
            await _context.SaveChangesAsync();
        }
    }
}
