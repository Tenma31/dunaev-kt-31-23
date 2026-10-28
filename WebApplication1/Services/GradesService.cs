using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GradesService : IGradesService
    {
        private readonly StudentDbContext _dbContext;

        public GradesService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<double> GetAverageGradeByDisciplineAsync(string disciplineName, CancellationToken cancellationToken)
        {
            return await _dbContext.Grades
                .Where(g => g.Discipline.DisciplineName == disciplineName)
                .AverageAsync(g => g.GradeValue, cancellationToken);
        }

        public async Task<double> GetAverageGradeByGroupAsync(string groupName, CancellationToken cancellationToken)
        {
            return await _dbContext.Grades
                .Where(g => g.Student.Group.GroupName == groupName)
                .AverageAsync(g => g.GradeValue, cancellationToken);
        }

        public async Task<IEnumerable<Grades>> GetGradesByStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            return await _dbContext.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Grades> AddOrUpdateGradeAsync(Grades grade, CancellationToken cancellationToken)
        {
            if (grade.GradeId == 0)
            {
                _dbContext.Grades.Add(grade);
            }
            else
            {
                _dbContext.Grades.Update(grade);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }
        public async Task<IEnumerable<string>> GetStudentGradesAsync(int studentId, CancellationToken cancellationToken)
        {
            return await _dbContext.Grades
                .Where(g => g.StudentId == studentId)
                .Select(g => g.Discipline.DisciplineName + ": " + g.GradeValue)
                .ToListAsync(cancellationToken);
        }

        public async Task<double> GetAverageGradeByYearAsync(int year, CancellationToken cancellationToken)
        {
            // В нашей базе нет поля "год", поэтому берем всех студентов, чей курс = year
            var grades = await _dbContext.Grades
                .Where(g => g.Student.Groups.GroupCourse == year)
                .ToListAsync(cancellationToken);

            return grades.Any() ? grades.Average(g => g.GradeValue) : 0;
        }
    }
}