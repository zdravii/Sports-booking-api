using Application.Commands.Court;
using Application.DTO.Courts;
using Application.Exceptions;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Courts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Courts
{
    public class CreateCourtCommand : UseCase, ICreateCourtCommand
    {
        private readonly CreateCourtValidator _validator;
        public CreateCourtCommand(DataBaseContext db,CreateCourtValidator validator) : base(db)
        {
            _validator = validator;
        }

        public string Id => "create-court";

        public string Name => "Create court";

        public void Execute(CreateCourtDTO request)
        {
            _validator.ValidateAndThrow(request);

            if (!_db.Sports.Any(s => s.Id == request.SportId))
            {
                throw new NotFoundException("Selected sport does not exist");
            }

            if (!_db.Centers.Any(c => c.Id == request.CenterId))
            {
                throw new NotFoundException("Selected center does not exist");
            }

            if (_db.Courts.Any(c => c.Label.ToLower() == request.Label.ToLower() && c.CenterId == request.CenterId))
            {
                throw new ConflictException("A court with this label already exists in the selected center.");
            }

            Domain.Entities.Court courtToAdd = new Domain.Entities.Court
            {
                Label = request.Label,
                CenterId = request.CenterId,
                SportId = request.SportId,
                isActive = true
            };

            _db.Courts.Add(courtToAdd);
            _db.SaveChanges();
        }
    }
}
