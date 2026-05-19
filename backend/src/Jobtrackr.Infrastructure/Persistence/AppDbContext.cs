using Jobtrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Jobtrackr.Application.Interfaces;

namespace Jobtrackr.Infrastructure.Persistence;

public class AppDbContext : DbContext, IApplicationDbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    public DbSet<ApplicationNote> ApplicationNotes => Set<ApplicationNote>();

    public DbSet<ApplicationStatusHistory> ApplicationStatusHistories
        => Set<ApplicationStatusHistory>();

    public DbSet<ApplicationTask> ApplicationTasks
    => Set<ApplicationTask>();    

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}