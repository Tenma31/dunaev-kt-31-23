using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly StudentDbContext _dbContext;

        public GroupsService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Groups>> GetGroupsAsync(GroupsFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Groups.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.SpecialtyName))
            {
                query = query.Where(g => g.Specialty.SpecialtyTitle.Contains(filter.SpecialtyName));
            }
            if (filter.Course.HasValue)
            {
                query = query.Where(g => g.GroupCourse == filter.Course.Value);
            }
            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(g => g.GroupIsDeleted == filter.IsDeleted.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Groups> AddGroupAsync(Groups group, CancellationToken cancellationToken)
        {
            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<Groups> UpdateGroupAsync(Groups group, CancellationToken cancellationToken)
        {
            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }

        public async Task<bool> DeleteGroupAsync(int id, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(new object[] { id }, cancellationToken);
            if (group == null) return false;

            group.GroupIsDeleted = true; // Мягкое удаление
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}