using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewDTO>
    {
        public CreateReviewValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(r => r.Rating).GreaterThan(0)
                                  .WithMessage("Rating cannot be lower then 1")
                                  .LessThan(6)
                                  .WithMessage("Rating cannot be higher then 5");

            RuleFor(r => r.Comment).NotEmpty()
                                   .WithMessage("Comment is required")
                                   .MaximumLength(300)
                                   .WithMessage("Comment cannot be longer than 300 characters");

            RuleFor(r => r.CourtId).GreaterThan(0)
                                   .WithMessage("Court id is required");
        }
    }
}
