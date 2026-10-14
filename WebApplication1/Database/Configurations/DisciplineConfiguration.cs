using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Database.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Database.Configurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Disciplines>
{
    private const string TableName = "cd_discipline";

    public void Configure(EntityTypeBuilder<Disciplines> builder)
    {
        builder.ToTable(TableName);

        builder
            .HasKey(discipline => discipline.DisciplineId)
            .HasName($"pk_{TableName}_discipline_id");

        builder.Property(discipline => discipline.DisciplineId)
            .HasColumnName("c_discipline_id")
            .HasColumnType(ColumnType.Int)
            .ValueGeneratedOnAdd();

        builder.Property(discipline => discipline.DisciplineName)
            .HasColumnName("c_discipline_name")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(discipline => discipline.DisciplineIsDeleted)
            .HasColumnName("c_discipline_is_deleted")
            .HasColumnType(ColumnType.Bool)
            .HasDefaultValue(false)
            .IsRequired();
    }
}