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
            // Настраиваем базу данных в памяти (InMemory)
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "student_test_db") // Название может быть любым
                .Options;
        }

        [Fact]
        public async Task GetStudentsAsync_GroupGr1_ReturnsTwoStudents()
        {
            // Arrange (Подготовка)
            // 1. Создаем контекст нашей тестовой БД
            using var ctx = new StudentDbContext(_dbContextOptions);

            // 2. Создаем тестовые данные
            var specialty = new Specialty { SpecialtyTitle = "сп1", SpecialtyCode = 1 };
            ctx.Specialties.Add(specialty);

            var group1 = new Groups { GroupName = "гр1", GroupCourse = 1, Specialty = specialty, GroupIsDeleted = false };
            var group2 = new Groups { GroupName = "гр2", GroupCourse = 1, Specialty = specialty, GroupIsDeleted = false };
            ctx.Groups.AddRange(group1, group2);
            await ctx.SaveChangesAsync(); // Сохраняем, чтобы группы получили свои ID

            // Добавляем студентов
            var students = new List<Student>
            {
                new Student { StudentFirstName = "иван", StudentLastName = "иванов", GroupId = group1.GroupId, StudentIsDeleted = false },
                new Student { StudentFirstName = "петр", StudentLastName = "иванов", GroupId = group2.GroupId, StudentIsDeleted = false },
                new Student { StudentFirstName = "макс", StudentLastName = "иванов", GroupId = group1.GroupId, StudentIsDeleted = false }
            };
            ctx.Students.AddRange(students);
            await ctx.SaveChangesAsync();

            // 3. Создаем сервис, который будем тестировать
            var studentService = new StudentsService(ctx);

            // Act (Действие)
            // Ищем студентов группы "гр1"
            var filter = new StudentsFilter { GroupName = "гр1" };
            var result = await studentService.GetStudentsAsync(filter, CancellationToken.None);

            // Assert (Проверка)
            // В группе "гр1" у нас 2 студента (иван и макс)
            Assert.Equal(2, result.Count());
        }
    }
}