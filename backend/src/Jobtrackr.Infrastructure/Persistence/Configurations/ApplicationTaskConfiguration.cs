using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtrackr.Infrastructure.Persistence.Configurations;

public class ApplicationTaskConfiguration
    : IEntityTypeConfiguration<ApplicationTask>
{
    public void Configure(
        EntityTypeBuilder<ApplicationTask> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.JobApplication)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.JobApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}