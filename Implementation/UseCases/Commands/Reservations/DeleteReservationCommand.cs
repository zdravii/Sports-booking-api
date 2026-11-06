using Application.Commands.Reservation;
using Application.Exceptions;
using Application.Services;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Reservations
{
    public class DeleteReservationCommand : UseCase,IDeleteReservationCommand
    {
        private readonly ICurrentUserService _currentUser;
        public DeleteReservationCommand(DataBaseContext db,ICurrentUserService currentUser) : base(db)
        {
            _currentUser = currentUser;
        }

        public string Id => "delete-reservation";

        public string Name => "Delete reservation";

        public void Execute(int id)
        {
            if (id <= 1)
            {
                throw new BadRequestException("Invalid id parameter");
            }

            Reservation? reservationToDelete = _db.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservationToDelete == null)
            {
                throw new NotFoundException("Reservation you are trying to delete does not exist");
            }

            if (reservationToDelete.UserId != _currentUser.UserId)
            {
                throw new ForbidenException("Reservation you are trying to delete is not yours");
            }

            if (reservationToDelete.StartTime <= DateTime.Now)
            {
                throw new ConflictException("Cannot delete reservation that already happend");
            }

            _db.Reservations.Remove(reservationToDelete);
            _db.SaveChanges();
        }
    }
}
