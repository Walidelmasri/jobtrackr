using FluentValidation;
using Jobtrackr.Application.DTOs;

namespace Jobtrackr.Application.Validators;

public class UpdateApplicationNoteRequestValidator
    : AbstractValidator<UpdateApplicationNoteRequest>
{
    public UpdateApplicationNoteRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000);
    }
}