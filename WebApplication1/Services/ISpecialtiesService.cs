using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ISpecialtiesService
    {
        Task<IEnumerable<Specialty>> GetSpecialtiesAsync(SpecialtiesFilter filter, CancellationToken ct);
        Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken ct);
        Task<Specialty> UpdateSpecialtyAsync(Specialty specialty, CancellationToken ct);
        Task<bool> DeleteSpecialtyAsync(int id, CancellationToken ct);
    }
}