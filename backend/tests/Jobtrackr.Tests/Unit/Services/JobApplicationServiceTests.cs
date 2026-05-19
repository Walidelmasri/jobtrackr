using Jobtrackr.Application.DTOs;
using Jobtrackr.Application.Services;
using Jobtrackr.Domain.Entities;
using Jobtrackr.Domain.Enums;
using Jobtrackr.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobtrackr.Tests.Unit.Services;

public class JobApplicationServiceTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateJobApplication()
    {
        using var context = CreateContext();

        var service = new JobApplicationService(context);

        var request = new CreateJobApplicationRequest
        {
            CompanyName = "Microsoft",
            CompanyWebsite = "https://microsoft.com",
            SponsorsVisa = true,
            RoleTitle = "Software Engineer",
            JobUrl = "https://careers.microsoft.com",
            SalaryMin = 40000,
            SalaryMax = 55000,
            WorkMode = WorkMode.Hybrid,
            EmploymentType = EmploymentType.FullTime
        };

        var result = await service.CreateAsync(request);

        Assert.Equal("Microsoft", result.CompanyName);
        Assert.Equal("Software Engineer", result.RoleTitle);
        Assert.Equal(ApplicationStatus.Saved, result.Status);
        Assert.Single(context.JobApplications);
        Assert.Single(context.Companies);
    }

    [Fact]
    public async Task CreateAsync_ShouldReuseExistingCompany_WhenCompanyNameAlreadyExists()
    {
        using var context = CreateContext();

        var existingCompany = new Company(
            "Microsoft",
            "https://microsoft.com",
            true);

        context.Companies.Add(existingCompany);
        await context.SaveChangesAsync();

        var service = new JobApplicationService(context);

        var request = new CreateJobApplicationRequest
        {
            CompanyName = "Microsoft",
            RoleTitle = "Backend Developer",
            WorkMode = WorkMode.Remote,
            EmploymentType = EmploymentType.FullTime
        };

        await service.CreateAsync(request);

        Assert.Single(context.Companies);
        Assert.Single(context.JobApplications);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenJobDoesNotExist()
    {
        using var context = CreateContext();

        var service = new JobApplicationService(context);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenJobDoesNotExist()
    {
        using var context = CreateContext();

        var service = new JobApplicationService(context);

        var result = await service.DeleteAsync(Guid.NewGuid());

        Assert.False(result);
    }
}