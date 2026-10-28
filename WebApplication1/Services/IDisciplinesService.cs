using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDisciplinesService
    {
        Task<IEnumerable<Disciplines>> GetDisciplinesAsync(DisciplinesFilter filter, CancellationToken cancellationToken);
        Task<Disciplines> AddDisciplineAsync(Disciplines discipline, CancellationToken cancellationToken);
        Task<Disciplines> UpdateDisciplineAsync(Disciplines discipline, CancellationToken cancellationToken);
        Task<bool> DeleteDisciplineAsync(int id, CancellationToken cancellationToken);
    }
}