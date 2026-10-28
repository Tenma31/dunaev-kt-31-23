using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly StudentDbContext _dbContext;

        public StudentsService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetStudentsAsync(StudentsFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Students.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
            {
                query = query.Where(s => s.Group.GroupName.Contains(filter.GroupName));
            }
            if (!string.IsNullOrWhiteSpace(filter.StudentFio))
            {
                var fio = filter.StudentFio.Trim().ToLower();
                query = query.Where(s => s.StudentFirstName.ToLower().Contains(fio)
                                      || s.StudentLastName.ToLower().Contains(fio));
            }
            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(s => s.StudentIsDeleted == filter.IsDeleted.Value);
            }

            return await query.Select(s => s.StudentLastName + " " + s.StudentFirstName).ToListAsync(cancellationToken);
        }

        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int id, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students.FindAsync(new object[] { id }, cancellationToken);
            if (student == null) return false;

            student.StudentIsDeleted = true; // Мягкое удаление
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<IEnumerable<string>> GetSpecialtiesByStudentLastNameAsync(string lastName, CancellationToken cancellationToken)
        {
            
            var specialties = await _dbContext.Students
                .Where(s => s.StudentLastName.ToLower().Contains(lastName.ToLower()))
                .Select(s => s.Group.Specialty.SpecialtyTitle) 
                .Distinct() 
                .ToListAsync(cancellationToken);

            return specialties;
        }
    }
}