using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtrackr.Infrastructure.Persistence.Configurations;

public class JobApplicationConfiguration
    : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(
        EntityTypeBuilder<JobApplication> builder)
    {
        builder.ToTable("JobApplications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RoleTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.JobUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.SalaryMin)
            .HasPrecision(18,2);

        builder.Property(x => x.SalaryMax)
            .HasPrecision(18,2);

        builder.Property(x => x.WorkMode)
            .HasConversion<string>();

        builder.Property(x => x.EmploymentType)
            .HasConversion<string>();

        builder.Property(x => x.Status)
            .HasConversion<string>();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.CompanyId);
    }
}