using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ISpecialtiesService
    {
        Task<IEnumerable<string>> GetSpecialtiesAsync(SpecialtiesFilter filter, CancellationToken ct);
        Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken ct);
    }

    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly StudentDbContext _dbContext;

        public SpecialtiesService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetSpecialtiesAsync(SpecialtiesFilter filter, CancellationToken ct)
        {
            var query = _dbContext.Specialties.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.SpecialtyTitle.Contains(filter.Title));

            if (filter.Code.HasValue)
                query = query.Where(s => s.SpecialtyCode == filter.Code.Value);

            // Выводим просто названия специальностей
            return await query.Select(s => s.SpecialtyTitle).ToListAsync(ct);
        }

        public async Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken ct)
        {
            _dbContext.Specialties.Add(specialty);
            await _dbContext.SaveChangesAsync(ct);
            return specialty;
        }
    }
}