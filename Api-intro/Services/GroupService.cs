using Api_intro.Data;
using Api_intro.DTOs.Groups;
using Api_intro.Helpers.Exceptions;
using Api_intro.Models;
using Api_intro.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroupService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            var groups = await _context.Groups.Include(g => g.Students).AsNoTracking().ToListAsync();
            return _mapper.Map<List<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            var group = await _context.Groups.Include(g => g.Students).AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
            if (group == null) throw new NotFoundException("Group not found");
            return _mapper.Map<GroupDto>(group);
        }

        public async Task CreateAsync(GroupCreateDto groupDto)
        {
            var group = _mapper.Map<Group>(groupDto);
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, GroupEditDto groupDto)
        {
            var existingGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id)
                ?? throw new NotFoundException("Group not found");

            _mapper.Map(groupDto, existingGroup);

            _context.Groups.Update(existingGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id) ?? throw new NotFoundException("Group not found");
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }
}
