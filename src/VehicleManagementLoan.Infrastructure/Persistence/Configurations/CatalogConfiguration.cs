using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Persistence.Configurations;

public sealed class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.ToTable("Catalogs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ParentCatalogId).IsRequired(false);

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(x => x.ParentCatalogId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Catalogs_Catalogs_ParentCatalogId");
    }
}
