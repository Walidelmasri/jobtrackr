using FluentValidation;
using Jobtrackr.Application.DTOs;

namespace Jobtrackr.Application.Validators;

public class CreateApplicationTaskRequestValidator
    : AbstractValidator<CreateApplicationTaskRequest>
{
    public CreateApplicationTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow);
    }
}