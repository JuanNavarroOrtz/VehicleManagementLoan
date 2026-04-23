using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Persistence.Configurations;

public sealed class MaintenanceRecordConfiguration : IEntityTypeConfiguration<MaintenanceRecord>
{
    public void Configure(EntityTypeBuilder<MaintenanceRecord> builder)
    {
        builder.ToTable("MaintenanceRecords");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.VehicleId).IsRequired();
        builder.Property(x => x.LoanId).IsRequired(false);
        builder.Property(x => x.MaintenanceContextTypeId).IsRequired();
        builder.Property(x => x.WorkTypeId).IsRequired();
        builder.Property(x => x.Kilometers).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Cost).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description).HasMaxLength(250);
        builder.Property(x => x.StatusId).IsRequired();
        builder.Property(x => x.Consecutive).HasMaxLength(50);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_MaintenanceRecords_Vehicles_VehicleId");

        builder.HasOne<Loan>()
            .WithMany()
            .HasForeignKey(x => x.LoanId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_MaintenanceRecords_Loans_LoanId");

        builder.HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(x => x.MaintenanceContextTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_MaintenanceRecords_Catalogs_MaintenanceContextTypeId");

        builder.HasOne<WorkType>()
            .WithMany()
            .HasForeignKey(x => x.WorkTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_MaintenanceRecords_WorkTypes_WorkTypeId");

        builder.HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_MaintenanceRecords_Catalogs_StatusId");
    }
}
