using Application.DTO.Courts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators.Courts
{
    public class CreateCourtValidator : AbstractValidator<CreateCourtDTO>
    {
        public CreateCourtValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Label).NotEmpty()
                                 .WithMessage("Court label is required.")
                                 .MinimumLength(2)
                                 .WithMessage("Court label must contain at least 2 characters.")
                                 .MaximumLength(50)
                                 .WithMessage("Court label cannot contain more than 50 characters.");

            RuleFor(x => x.SportId).GreaterThan(0)
                                   .WithMessage("Sport is required");

            RuleFor(x => x.CenterId).GreaterThan(0)
                                   .WithMessage("Center is required");
        }
    }
}
