using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtrackr.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration
    : IEntityTypeConfiguration<Company>
{
    public void Configure(
        EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Website)
            .HasMaxLength(500);

        builder.Property(x => x.SponsorsVisa)
            .IsRequired();

        builder.HasMany(x => x.Applications)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId);
    }
}