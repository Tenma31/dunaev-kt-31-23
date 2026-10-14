using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Database
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Disciplines> Disciplines { get; set; }
        public DbSet<Grades> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentDbContext).Assembly);
        }
    }
}