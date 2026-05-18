using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobtrackr.Infrastructure.Persistence.Configurations;

public class ApplicationNoteConfiguration
    : IEntityTypeConfiguration<ApplicationNote>
{
    public void Configure(
        EntityTypeBuilder<ApplicationNote> builder)
    {
        builder.ToTable("ApplicationNotes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.JobApplication)
            .WithMany(x => x.Notes)
            .HasForeignKey(x => x.JobApplicationId);
    }
}