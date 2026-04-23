using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Persistence.Configurations;

public sealed class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bills");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LoanId).IsRequired();
        builder.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Tax).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Total).HasColumnType("decimal(18,2)");
        builder.Property(x => x.BillStatusId).IsRequired();

        builder.HasOne<Loan>()
            .WithMany()
            .HasForeignKey(x => x.LoanId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Bills_Loans_LoanId");

        builder.HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(x => x.BillStatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Bills_Catalogs_BillStatusId");
    }
}
