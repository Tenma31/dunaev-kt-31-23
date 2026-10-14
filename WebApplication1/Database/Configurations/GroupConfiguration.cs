using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;
using WebApplication1.Database.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Database.Configurations
{
    public class GroupsConfiguration : IEntityTypeConfiguration<Groups>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Groups> builder)
        {
            builder.ToTable(TableName);

            builder
                .HasKey(group => group.GroupId)
                .HasName($"pk_{TableName}_group_id");

            builder.Property(group => group.GroupId)
                .HasColumnName("c_group_id")
                .HasColumnType(ColumnType.Int)
                .ValueGeneratedOnAdd();

            builder.Property(group => group.GroupName)
                .HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(group => group.GroupCourse)
                .HasColumnName("c_group_course")
                .HasColumnType(ColumnType.Int)
                .IsRequired();

            builder.Property(group => group.SpecialtyId)
                .HasColumnName("f_specialty_id")
                .HasColumnType(ColumnType.Int)
                .IsRequired();

            builder.Property(group => group.GroupIsDeleted)
                .HasColumnName("c_group_is_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .HasOne(group => group.Specialty)
                .WithMany(specialty => specialty.Groups)
                .HasForeignKey(group => group.SpecialtyId)
                .HasConstraintName("fk_f_specialty_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(group => group.SpecialtyId)
                .HasDatabaseName($"idx_{TableName}_f_specialty_id");

            builder.Navigation(group => group.Specialty)
                .AutoInclude();
        }
    }
}