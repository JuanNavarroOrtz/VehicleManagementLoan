using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Persistence.Configurations;

public sealed class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.VehicleId).IsRequired();
        builder.Property(x => x.ClientId).IsRequired();
        builder.Property(x => x.FeeId).IsRequired();
        builder.Property(x => x.StatusId).IsRequired();
        builder.Property(x => x.Deposit).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Consecutive).HasMaxLength(50);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Loans_Vehicles_VehicleId");

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Loans_Clients_ClientId");

        builder.HasOne<Fee>()
            .WithMany()
            .HasForeignKey(x => x.FeeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Loans_Fees_FeeId");

        builder.HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Loans_Catalogs_StatusId");
    }
}
