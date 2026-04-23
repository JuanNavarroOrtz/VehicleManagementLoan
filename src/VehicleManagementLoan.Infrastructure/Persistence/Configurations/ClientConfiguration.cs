using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Persistence.Configurations;

public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.SecondName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.SecondLastName).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(250).IsRequired();
        builder.Property(x => x.CityId).IsRequired();
        builder.Property(x => x.BusinessName).HasMaxLength(150);
        builder.Property(x => x.CommercialName).HasMaxLength(150);
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x => x.IdentificationNumber).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Phone).HasMaxLength(30).IsRequired();
        builder.Property(x => x.ClientTypeId).IsRequired();

        builder.HasIndex(x => x.IdentificationNumber).IsUnique();

        builder.HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Clients_Catalogs_CityId");

        builder.HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(x => x.ClientTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Clients_Catalogs_ClientTypeId");
    }
}
