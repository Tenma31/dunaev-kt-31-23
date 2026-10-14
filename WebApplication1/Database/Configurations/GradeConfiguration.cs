using WebApplication1.Database.Helpers;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace WebApplication1.Database.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grades>
{
    private const string TableName = "cd_grade";

    public void Configure(EntityTypeBuilder<Grades> builder)
    {
        builder.ToTable(TableName);

        builder
            .HasKey(grade => grade.GradeId)
            .HasName($"pk_{TableName}_grade_id");

        builder.Property(grade => grade.GradeId)
            .HasColumnName("c_grade_id")
            .HasColumnType(ColumnType.Int)
            .ValueGeneratedOnAdd();

        builder.Property(grade => grade.GradeValue)
            .HasColumnName("c_grade_value")
            .HasColumnType(ColumnType.Int)
            .IsRequired();

        builder.Property(grade => grade.StudentId)
            .HasColumnName("f_student_id")
            .HasColumnType(ColumnType.Int)
            .IsRequired();

        builder.Property(grade => grade.DisciplineId)
            .HasColumnName("f_discipline_id")
            .HasColumnType(ColumnType.Int)
            .IsRequired();

        builder
            .HasOne(grade => grade.Student)
            .WithMany(student => student.Grades)
            .HasForeignKey(grade => grade.StudentId)
            .HasConstraintName("fk_f_student_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(grade => grade.StudentId)
            .HasDatabaseName($"idx_{TableName}_f_student_id");

        builder
            .HasOne(grade => grade.Discipline)
            .WithMany(discipline => discipline.Grades)
            .HasForeignKey(grade => grade.DisciplineId)
            .HasConstraintName("fk_f_discipline_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(grade => grade.DisciplineId)
            .HasDatabaseName($"idx_{TableName}_f_discipline_id");

        builder.Navigation(grade => grade.Student)
            .AutoInclude();

        builder.Navigation(grade => grade.Discipline)
            .AutoInclude();
    }
}