using Application.Commands.Court;
using Application.Exceptions;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace Implementation.UseCases.Commands.Court
{
    public class DeleteCourtCommand : UseCase, IDeleteCourtCommand
    {
        public DeleteCourtCommand(DataBaseContext db) : base(db)
        {
        }

        public string Id => "delete-court";

        public string Name => "Delete court";

        public void Execute(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid parameter");
            }

            Domain.Entities.Court? courtToDelete = _db.Courts.FirstOrDefault(c => c.Id == id);

            if (courtToDelete == null)
            {
                throw new NotFoundException("Court does not exist");
            }

            _db.Courts.Remove(courtToDelete);
            _db.SaveChanges();
        }
    }
}
