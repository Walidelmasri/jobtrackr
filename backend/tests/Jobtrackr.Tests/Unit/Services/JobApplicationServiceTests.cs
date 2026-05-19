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
    [Fact]
    public async Task GetAllAsync_ShouldSearchByCompanyName()
    {
        using var context = CreateContext();

        var microsoft = new Company(
            "Microsoft",
            "",
            true);

        var google = new Company(
            "Google",
            "",
            true);

        context.Companies.AddRange(
            microsoft,
            google);

        context.JobApplications.AddRange(
            new JobApplication(
                microsoft.Id,
                "Backend",
                WorkMode.Hybrid,
                EmploymentType.FullTime),

            new JobApplication(
                google.Id,
                "Frontend",
                WorkMode.Hybrid,
                EmploymentType.FullTime));

        await context.SaveChangesAsync();

        var service = new JobApplicationService(context);

        var query =
            new JobApplicationQueryParameters
            {
                Search = "Microsoft"
            };

        var result =
            await service.GetAllAsync(query);

        Assert.Single(result.Items);

        Assert.Equal(
            "Microsoft",
            result.Items[0].CompanyName);
    }

    [Fact]
    public async Task GetAllAsync_ShouldPaginate()
    {
        using var context = CreateContext();

        var company =
            new Company(
                "Microsoft",
                "",
                true);

        context.Companies.Add(company);

        for (var i = 0; i < 15; i++)
        {
            context.JobApplications.Add(
                new JobApplication(
                    company.Id,
                    $"Role {i}",
                    WorkMode.Hybrid,
                    EmploymentType.FullTime));
        }

        await context.SaveChangesAsync();

        var service =
            new JobApplicationService(context);

        var query =
            new JobApplicationQueryParameters
            {
                Page = 2,
                PageSize = 10
            };

        var result =
            await service.GetAllAsync(query);

        Assert.Equal(5, result.Items.Count);

        Assert.Equal(15, result.TotalCount);
    }
}