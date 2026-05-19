using FluentValidation;
using Jobtrackr.Application.DTOs;

namespace Jobtrackr.Application.Validators;

public class CreateJobApplicationRequestValidator
    : AbstractValidator<CreateJobApplicationRequest>
{
    public CreateJobApplicationRequestValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.RoleTitle)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.SalaryMax)
            .GreaterThanOrEqualTo(x => x.SalaryMin)
            .When(x =>
                x.SalaryMin.HasValue &&
                x.SalaryMax.HasValue);
    }
}