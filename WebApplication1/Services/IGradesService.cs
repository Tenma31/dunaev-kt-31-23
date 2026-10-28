using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IGradesService
    {
        Task<double> GetAverageGradeByDisciplineAsync(string disciplineName, CancellationToken cancellationToken);
        Task<double> GetAverageGradeByGroupAsync(string groupName, CancellationToken cancellationToken);
        Task<IEnumerable<Grades>> GetGradesByStudentAsync(int studentId, CancellationToken cancellationToken);
        Task<Grades> AddOrUpdateGradeAsync(Grades grade, CancellationToken cancellationToken);
        Task<IEnumerable<string>> GetStudentGradesAsync(int studentId, CancellationToken ct);
        Task<double> GetAverageGradeByYearAsync(int year, CancellationToken ct);
    }
}