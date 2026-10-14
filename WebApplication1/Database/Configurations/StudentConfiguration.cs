using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Database.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const string TableName = "cd_student";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(TableName);

            builder
                .HasKey(student => student.StudentId)
                .HasName($"pk_{TableName}_student_id");

            builder.Property(student => student.StudentId)
                .HasColumnName("c_student_id")
                .HasColumnType(ColumnType.Int)
                .ValueGeneratedOnAdd();

            builder.Property(student => student.StudentFirstName)
                .HasColumnName("c_student_firstname")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(student => student.StudentLastName)
                .HasColumnName("c_student_lastname")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(student => student.GroupId)
                .HasColumnName("f_group_id")
                .HasColumnType(ColumnType.Int)
                .IsRequired();

            builder.Property(student => student.StudentIsDeleted)
                .HasColumnName("c_student_is_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .HasOne(student => student.Group)
                .WithMany(group => group.Students)
                .HasForeignKey(student => student.GroupId)
                .HasConstraintName("fk_f_group_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(student => student.GroupId)
                .HasDatabaseName($"idx_{TableName}_f_group_id");

            builder.Navigation(student => student.Group)
                .AutoInclude();
        }
    }
}