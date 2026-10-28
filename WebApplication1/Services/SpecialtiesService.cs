using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly StudentDbContext _dbContext;

        public SpecialtiesService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Specialty>> GetSpecialtiesAsync(SpecialtiesFilter filter, CancellationToken ct)
        {
            var query = _dbContext.Specialties.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(s => s.SpecialtyTitle.Contains(filter.Title));

            if (filter.Code.HasValue)
                query = query.Where(s => s.SpecialtyCode == filter.Code.Value);

            return await query.ToListAsync(ct);
        }

        public async Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken ct)
        {
            _dbContext.Specialties.Add(specialty);
            await _dbContext.SaveChangesAsync(ct);
            return specialty;
        }


        public async Task<Specialty> UpdateSpecialtyAsync(Specialty specialty, CancellationToken ct)
        {
            _dbContext.Specialties.Update(specialty);
            await _dbContext.SaveChangesAsync(ct);
            return specialty;
        }

        public async Task<bool> DeleteSpecialtyAsync(int id, CancellationToken ct)
        {
            var specialty = await _dbContext.Specialties.FindAsync(new object[] { id }, ct);
            if (specialty == null) return false;
            _dbContext.Specialties.Remove(specialty);
            await _dbContext.SaveChangesAsync(ct);
            return true;
        }
    }
}