using Application.Commands.Center;
using Application.Exceptions;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Center
{
    public class DeleteCenterCommand : UseCase, IDeleteCenterCommand
    {
        public DeleteCenterCommand(DataBaseContext db) : base(db)
        {
        }

        public string Id => "delete-center";

        public string Name => "Delete center";

        public void Execute(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid parameter");
            }

            Domain.Entities.Center? centerToDelete = _db.Centers.FirstOrDefault(c => c.Id == id);

            if (centerToDelete == null)
            {
                throw new NotFoundException("Seleceted Center does not exist");
            }

            _db.Centers.Remove(centerToDelete);
            _db.SaveChanges();
        }
    }
}
