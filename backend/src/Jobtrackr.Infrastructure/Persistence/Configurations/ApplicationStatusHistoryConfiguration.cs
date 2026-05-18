using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtrackr.Infrastructure.Persistence.Configurations;

public class ApplicationStatusHistoryConfiguration
    : IEntityTypeConfiguration<ApplicationStatusHistory>
{
    public void Configure(
        EntityTypeBuilder<ApplicationStatusHistory> builder)
    {
        builder.ToTable("ApplicationStatusHistories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FromStatus)
            .HasConversion<string>();

        builder.Property(x => x.ToStatus)
            .HasConversion<string>();

        builder.Property(x => x.ChangedAt)
            .IsRequired();

        builder.HasOne(x => x.JobApplication)
            .WithMany()
            .HasForeignKey(x => x.JobApplicationId);
    }
}