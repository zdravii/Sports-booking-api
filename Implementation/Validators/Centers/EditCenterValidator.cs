using Application.DTO.Center;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators.Centers
{
    public class EditCenterValidator : AbstractValidator<EditCenterDTO>
    {
        public EditCenterValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CenterId).GreaterThan(0)
                                    .WithMessage("Invalid center id");

            RuleFor(x => x.Name).MinimumLength(4)
                                .WithMessage("Name of center must contain at least 4 characters")
                                .MaximumLength(30)
                                .WithMessage("Name of center must contain less than 30 characters")
                                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Address).MaximumLength(100)
                                   .WithMessage("Address of center must contain less than 100 characters")
                                   .When(x => !string.IsNullOrWhiteSpace(x.Address));

            RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.Address))
                           .WithMessage("At least one field must be provided for update");
        }
    }
}
