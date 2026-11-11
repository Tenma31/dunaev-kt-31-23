using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Services;
using Xunit;

namespace WebApplication1.Tests
{
    public class StudentIntegrationTests
    {
        private readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public StudentIntegrationTests()
        {
            // Уникальная БД для каждого теста
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetStudentsAsync_Group_ReturnsOneActiveStudent()
        {
            using var ctx = new StudentDbContext(_dbContextOptions);

            var specialty = new Specialty { SpecialtyTitle = "сп1", SpecialtyCode = 1 };
            ctx.Specialties.Add(specialty);

            var group1 = new Groups { GroupName = "гр1", GroupCourse = 1, Specialty = specialty, GroupIsDeleted = false };
            var group2 = new Groups { GroupName = "гр2", GroupCourse = 1, Specialty = specialty, GroupIsDeleted = false };
            ctx.Groups.AddRange(group1, group2);
            await ctx.SaveChangesAsync();

            var students = new List<Student>
            {
                new Student { StudentFirstName = "иван", StudentLastName = "иванов", GroupId = group1.GroupId, StudentIsDeleted = false }, 
                new Student { StudentFirstName = "петр", StudentLastName = "иванов", GroupId = group2.GroupId, StudentIsDeleted = true }, 
                new Student { StudentFirstName = "макс", StudentLastName = "иванов", GroupId = group1.GroupId, StudentIsDeleted = false }   
            };
            ctx.Students.AddRange(students);
            await ctx.SaveChangesAsync();

            var studentService = new StudentsService(ctx);

            var filter = new StudentsFilter { GroupName = "гр1" };
            var result = await studentService.GetStudentsAsync(filter, CancellationToken.None);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetSpecialtiesByStudentLastNameAsync_Ivanov_ReturnsSp1()
        {
            using var ctx = new StudentDbContext(_dbContextOptions);

            var specialties = new List<Specialty>
            {
                new Specialty { SpecialtyTitle = "сп1", SpecialtyCode = 1 },
                new Specialty { SpecialtyTitle = "сп2", SpecialtyCode = 2 }
            };
            await ctx.Specialties.AddRangeAsync(specialties);
            await ctx.SaveChangesAsync();

            var groups = new List<Groups>
            {
                new Groups { GroupName = "гр1", GroupCourse = 1, SpecialtyId = 1, GroupIsDeleted = false },
                new Groups { GroupName = "гр2", GroupCourse = 1, SpecialtyId = 1, GroupIsDeleted = false },
                new Groups { GroupName = "гр3", GroupCourse = 1, SpecialtyId = 2, GroupIsDeleted = false }
            };
            await ctx.Groups.AddRangeAsync(groups);
            await ctx.SaveChangesAsync();

            var students = new List<Student>
            {
                new Student { StudentFirstName = "Иван", StudentLastName = "Иванов", GroupId = 1, StudentIsDeleted = true }, //сп1
                new Student { StudentFirstName = "Петр", StudentLastName = "Иванов", GroupId = 2, StudentIsDeleted = false }, //сп1
                new Student { StudentFirstName = "Макс", StudentLastName = "Иванов", GroupId = 2, StudentIsDeleted = false },  //сп1
                new Student { StudentFirstName = "Сергей", StudentLastName = "Сидоров", GroupId = 3, StudentIsDeleted = false } //сп2
            };
            await ctx.Students.AddRangeAsync(students);
            await ctx.SaveChangesAsync();

            var studentService = new StudentsService(ctx);

            var result = await studentService.GetSpecialtiesByStudentLastNameAsync("Иванов", CancellationToken.None);

            //Assert.Single(result);
            Assert.Equal(1, result.Count());
        }
    }
}