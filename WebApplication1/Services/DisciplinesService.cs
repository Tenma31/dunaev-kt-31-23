using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DisciplinesService : IDisciplinesService
    {
        private readonly StudentDbContext _dbContext;

        public DisciplinesService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Disciplines>> GetDisciplinesAsync(DisciplinesFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Disciplines.AsNoTracking();

            // Примечание: В текущей модели Disciplines нет поля для направления (гуманитарное/техническое).
            // Если преподаватель требует строгой реализации, нужно добавить поле Direction в модель и сделать миграцию.
            // Пока фильтры IsHumanitarian/IsTechnical игнорируются.

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(d => d.DisciplineIsDeleted == filter.IsDeleted.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Disciplines> AddDisciplineAsync(Disciplines discipline, CancellationToken cancellationToken)
        {
            _dbContext.Disciplines.Add(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<Disciplines> UpdateDisciplineAsync(Disciplines discipline, CancellationToken cancellationToken)
        {
            _dbContext.Disciplines.Update(discipline);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }

        public async Task<bool> DeleteDisciplineAsync(int id, CancellationToken cancellationToken)
        {
            var discipline = await _dbContext.Disciplines.FindAsync(new object[] { id }, cancellationToken);
            if (discipline == null) return false;

            discipline.DisciplineIsDeleted = true; // Мягкое удаление
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}