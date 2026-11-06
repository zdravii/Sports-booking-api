using Application.DTO.Sport;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators.Sports
{
    public class EditSportValidator : AbstractValidator<EditSportDTO>
    {
        public EditSportValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.SportId).GreaterThan(0)
                                   .WithMessage("Invalid sport id");

            RuleFor(x => x.Name).MinimumLength(4)
                                .WithMessage("Sport name must be at least 4 characters long.")
                                .MaximumLength(50)
                                .WithMessage("Sport name cannot be longer than 50 characters.")
                                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Description).MinimumLength(20)
                                       .WithMessage("Sport description must be at least 20 characters long.")
                                       .MaximumLength(500)
                                       .WithMessage("Sport description cannot be longer than 500 characters.")
                                       .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x).Must(x =>
                           !string.IsNullOrWhiteSpace(x.Name) ||
                           !string.IsNullOrWhiteSpace(x.Description))
                           .WithMessage("At least one field must be provided for update.");
        }
    }
}
