using Application.Commands.Reservation;
using Application.DTO.Reservation;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Application.Services;

namespace Implementation.UseCases.Commands.Reservations
{
    public class CreateReservationCommand : UseCase, ICreateReservationCommand
    {
        private readonly CreateReservationValidator _validator;
        private readonly ICurrentUserService _currentUser;
        public CreateReservationCommand(DataBaseContext db,CreateReservationValidator validator,ICurrentUserService currentUser) : base(db)
        {
            _currentUser = currentUser;
            _validator = validator;
        }

        public string Id => "create-reservation";

        public string Name => "Create reservation";

        public void Execute(CreateReservationDTO request)
        {
            _validator.ValidateAndThrow(request);

            Domain.Entities.Court? court = _db.Courts.FirstOrDefault(c => c.Id == request.CourtId);

            if (court == null)
            {
                throw new NotFoundException("Selected court does not exist");
            }

            if (!court.isActive)
            {
                throw new ConflictException("Selected court is not active at the moment");
            }

            if (_db.Reservations.Any(r => r.StartTime == request.StartTime && r.CourtId == request.CourtId))
            {
                throw new ConflictException("There is already a reservation for selected court and time");
            }

            Domain.Entities.Reservation newReservation = new Reservation
            {
                StartTime = request.StartTime,
                CourtId = request.CourtId,
                Status = Domain.Enums.ReservationStatus.Confirmed,
                UserId = _currentUser.UserId
            };

            _db.Reservations.Add(newReservation);
            _db.SaveChanges();
        }
    }
}
