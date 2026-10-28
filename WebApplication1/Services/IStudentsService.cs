using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IStudentsService
    {
        Task<IEnumerable<Student>> GetStudentsAsync(StudentsFilter filter, CancellationToken cancellationToken);
        Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken);
        Task<Student> UpdateStudentAsync(Student student, CancellationToken cancellationToken);
        Task<bool> DeleteStudentAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<string>> GetSpecialtiesByStudentLastNameAsync(string lastName, CancellationToken cancellationToken);
    }
}