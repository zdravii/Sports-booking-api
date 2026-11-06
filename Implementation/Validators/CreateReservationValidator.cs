using Application.DTO.Reservation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDTO>
    {
        public CreateReservationValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.StartTime).NotEmpty()
                                     .WithMessage("Start of reservation is required")
                                     .Must(x => x > DateTime.Now)
                                     .WithMessage("Start of reservation cannot be in past")
                                     .Must(x => x.Minute == 0 && x.Second == 0)
                                     .WithMessage("Reservation must start at the beginning of an hour");

            RuleFor(x => x.CourtId).GreaterThan(0)
                                   .WithMessage("Court id is required");
        }
    }
}
