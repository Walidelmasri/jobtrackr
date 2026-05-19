using FluentValidation.TestHelper;
using Jobtrackr.Application.DTOs;
using Jobtrackr.Application.Validators;
using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Tests.Unit.Validators;

public class CreateJobApplicationRequestValidatorTests
{
    private readonly CreateJobApplicationRequestValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenCompanyNameIsEmpty()
    {
        var request = new CreateJobApplicationRequest
        {
            CompanyName = "",
            RoleTitle = "Software Engineer",
            WorkMode = WorkMode.Hybrid,
            EmploymentType = EmploymentType.FullTime
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.CompanyName);
    }

    [Fact]
    public void ShouldHaveError_WhenRoleTitleIsEmpty()
    {
        var request = new CreateJobApplicationRequest
        {
            CompanyName = "Microsoft",
            RoleTitle = "",
            WorkMode = WorkMode.Hybrid,
            EmploymentType = EmploymentType.FullTime
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.RoleTitle);
    }

    [Fact]
    public void ShouldHaveError_WhenSalaryMinIsGreaterThanSalaryMax()
    {
        var request = new CreateJobApplicationRequest
        {
            CompanyName = "Microsoft",
            RoleTitle = "Software Engineer",
            SalaryMin = 60000,
            SalaryMax = 50000,
            WorkMode = WorkMode.Hybrid,
            EmploymentType = EmploymentType.FullTime
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.SalaryMax);
    }

    [Fact]
    public void ShouldNotHaveError_WhenRequestIsValid()
    {
        var request = new CreateJobApplicationRequest
        {
            CompanyName = "Microsoft",
            RoleTitle = "Software Engineer",
            SalaryMin = 40000,
            SalaryMax = 55000,
            WorkMode = WorkMode.Hybrid,
            EmploymentType = EmploymentType.FullTime
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }
}