using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jobtrackr.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }

    DbSet<JobApplication> JobApplications { get; }

    DbSet<ApplicationNote> ApplicationNotes { get; }

    DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; }
    DbSet<ApplicationTask> ApplicationTasks { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);
}