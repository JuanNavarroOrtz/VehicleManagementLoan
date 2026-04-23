using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Persistence.Configurations;

public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Plate).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Vin).HasMaxLength(50).IsRequired();
        builder.Property(x => x.BrandId).IsRequired();
        builder.Property(x => x.Model).HasMaxLength(100).IsRequired();
        builder.Property(x => x.VehicleTypeId).IsRequired();
        builder.Property(x => x.StatusId).IsRequired();

        builder.HasIndex(x => x.Plate).IsUnique();
        builder.HasIndex(x => x.Vin).IsUnique();

        builder.HasOne(v => v.Brand)
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicles_Catalogs_BrandId");

        builder.HasOne(v => v.VehicleType)
            .WithMany()
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicles_Catalogs_VehicleTypeId");

        builder.HasOne(v => v.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Vehicles_Catalogs_StatusId");
    }
}
