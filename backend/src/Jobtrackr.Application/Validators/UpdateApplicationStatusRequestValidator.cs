using FluentValidation;
using Jobtrackr.Application.DTOs;

namespace Jobtrackr.Application.Validators;

public class UpdateApplicationStatusRequestValidator
    : AbstractValidator<UpdateApplicationStatusRequest>
{
    public UpdateApplicationStatusRequestValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}