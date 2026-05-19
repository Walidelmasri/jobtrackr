using FluentValidation;
using Jobtrackr.Application.DTOs;

namespace Jobtrackr.Application.Validators;

public class CreateApplicationNoteRequestValidator
    : AbstractValidator<CreateApplicationNoteRequest>
{
    public CreateApplicationNoteRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000);
    }
}