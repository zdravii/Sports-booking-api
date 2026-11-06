using Application.DTO.Center;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators.Centers
{
    public class CreateCenterValidator : AbstractValidator<CreateCenterDTO>
    {
        public CreateCenterValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Name of center is required")
                                .MinimumLength(4)
                                .WithMessage("Name of center must contain at least 4 characters")
                                .MaximumLength(30)
                                .WithMessage("Name of center must contain less than 30 characters");

            RuleFor(x => x.Address).NotEmpty()
                                   .WithMessage("Address of center is required")
                                   .MaximumLength(100)
                                   .WithMessage("Address of center must contain less than 100 characters");
        }
    }
}
