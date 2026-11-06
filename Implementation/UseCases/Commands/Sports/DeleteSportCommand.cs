using Application.Commands.Sport;
using Application.Exceptions;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Sport
{
    public class DeleteSportCommand : UseCase, IDeleteSportCommand
    {
        public DeleteSportCommand(DataBaseContext db) : base(db)
        {
        }

        public string Id => "delete-sport";

        public string Name => "Delete sport";

        public void Execute(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid parameter");
            }

            Domain.Entities.Sport? sportToDelete = _db.Sports.FirstOrDefault(s => s.Id == id);

            if (sportToDelete == null)
            {
                throw new NotFoundException("Sport you want to delete does not exist");
            }

            _db.Sports.Remove(sportToDelete);
            _db.SaveChanges();
        }
    }
}
