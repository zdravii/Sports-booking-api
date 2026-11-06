using Application.DTO.Sport;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators.Sports
{
    public class CreateSportValidator : AbstractValidator<CreateSportDTO>
    {
        public CreateSportValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Sport name is required.")
                                .MinimumLength(4)
                                .WithMessage("Sport name must be at least 4 characters long.")
                                .MaximumLength(50)
                                .WithMessage("Sport name cannot be longer than 50 characters.");

            RuleFor(x => x.Description).NotEmpty()
                                       .WithMessage("Description is required")
                                       .MinimumLength(20)
                                       .WithMessage("Sport description must be at least 20 characters long.")
                                       .MaximumLength(500)
                                       .WithMessage("Sport description cannot be longer than 500 characters.");
        }
    }
}
