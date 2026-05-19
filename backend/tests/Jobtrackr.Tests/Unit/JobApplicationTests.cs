using Jobtrackr.Domain.Entities;
using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Tests.Unit;

public class JobApplicationTests
{
    [Fact]
    public void Constructor_ShouldSetDefaultStatusToSaved()
    {
        var job = new JobApplication(
            Guid.NewGuid(),
            "Software Engineer",
            WorkMode.Hybrid,
            EmploymentType.FullTime);

        Assert.Equal(ApplicationStatus.Saved, job.Status);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenRoleTitleIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
            new JobApplication(
                Guid.NewGuid(),
                "",
                WorkMode.Hybrid,
                EmploymentType.FullTime));
    }

    [Fact]
    public void UpdateSalary_ShouldThrow_WhenMinimumSalaryExceedsMaximumSalary()
    {
        var job = new JobApplication(
            Guid.NewGuid(),
            "Software Engineer",
            WorkMode.Hybrid,
            EmploymentType.FullTime);

        Assert.Throws<ArgumentException>(() =>
            job.UpdateSalary(60000, 50000));
    }

    [Fact]
    public void ChangeStatus_ShouldSetAppliedAt_WhenStatusChangesToApplied()
    {
        var job = new JobApplication(
            Guid.NewGuid(),
            "Software Engineer",
            WorkMode.Hybrid,
            EmploymentType.FullTime);

        job.ChangeStatus(ApplicationStatus.Applied);

        Assert.Equal(ApplicationStatus.Applied, job.Status);
        Assert.NotNull(job.AppliedAt);
    }
}