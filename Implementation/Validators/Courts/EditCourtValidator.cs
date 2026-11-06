using Application.DTO.Court;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators.Courts
{
    public class EditCourtValidator : AbstractValidator<EditCourtDTO>
    {
        public EditCourtValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CourtId).GreaterThan(0)
                                   .WithMessage("Invalid court id");

            RuleFor(x => x.Label).MinimumLength(2)
                                 .WithMessage("Court label must contain at least 2 characters.")
                                 .MaximumLength(50)
                                 .WithMessage("Court label cannot contain more than 50 characters.")
                                 .When(x => !string.IsNullOrWhiteSpace(x.Label));

            RuleFor(x => x.SportId).GreaterThan(0)
                                   .WithMessage("Invalid sport id")
                                   .When(x => x.SportId.HasValue);

            RuleFor(x => x.CenterId).GreaterThan(0)
                                    .WithMessage("Invalid center id")
                                    .When(x => x.CenterId.HasValue);

            RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x.Label) || x.SportId.HasValue || x.CenterId.HasValue)
                           .WithMessage("At least one field must be provided for update");
        }
    }
}
