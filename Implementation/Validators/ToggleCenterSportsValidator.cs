using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class ToggleCenterSportsValidator : AbstractValidator<ToggleCenterSportsDTO>
    {
        public ToggleCenterSportsValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CenterId).GreaterThan(0)
                                    .WithMessage("Center id is required");

            RuleFor(x => x.SportIds).NotNull()
                                    .WithMessage("Sport ids list is required")
                                    .Must(x => x.Distinct().Count() == x.Count())
                                    .WithMessage("Duplicate sport ids is not allowed")
                                    .When(x => x.SportIds != null);

            RuleForEach(x => x.SportIds).GreaterThan(0)
                                        .WithMessage("Each sport id must be a valid,positive number");
        }
    }
}
