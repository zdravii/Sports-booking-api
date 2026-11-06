using Application.Commands.Court;
using Application.DTO.Court;
using DataAccess;
using FluentValidation;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Implementation.Validators.Courts;

namespace Implementation.UseCases.Commands.Court
{
    public class EditCourtCommand : UseCase, IEditCourtCommand
    {
        private readonly EditCourtValidator _validator;

        public EditCourtCommand(DataBaseContext db,EditCourtValidator validator) : base(db)
        {
            _validator = validator;
        }

        public string Id => "edit-court";

        public string Name => "Edit court";

        public void Execute(EditCourtDTO request)
        {
            _validator.ValidateAndThrow(request);

            Domain.Entities.Court? courtToEdit = _db.Courts.FirstOrDefault(c => c.Id == request.CourtId);

            if (courtToEdit == null)
            {
                throw new NotFoundException("Court you are trying to edit does not exist");
            }

            if (request.CenterId.HasValue && courtToEdit.CenterId != request.CenterId.Value)
            {
                if (!_db.Centers.Any(c => c.Id == request.CenterId.Value))
                {
                    throw new NotFoundException("Invalid center id");
                }
            }

            if (request.SportId.HasValue && courtToEdit.SportId != request.SportId.Value)
            {
                if (!_db.Sports.Any(s => s.Id == request.SportId.Value))
                {
                    throw new NotFoundException("Invalid sport id");
                }
            }

            int newCenterId = request.CenterId ?? courtToEdit.CenterId;
            int newSportId = request.SportId ?? courtToEdit.SportId;

            if (request.SportId.HasValue || request.CenterId.HasValue)
            {
                if (!_db.CenterSports.Any(cs => cs.SportId == newSportId && cs.CenterId == newCenterId))
                {
                    throw new ConflictException("Center does not provide selected sport");
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Label))
            {
                if (_db.Courts.Any(c => c.Id != request.CourtId && c.Label == request.Label && c.CenterId == newCenterId))
                {
                    throw new ConflictException("Court with provided label already exist in selected center");
                }
            }

            if (request.isActive.HasValue)
            {
                courtToEdit.isActive = request.isActive.Value;
            }

            if (!string.IsNullOrWhiteSpace(request.Label))
            {
                courtToEdit.Label = request.Label;
            }
            if (request.CenterId.HasValue)
            {
                courtToEdit.CenterId = request.CenterId.Value;
            }
            if (request.SportId.HasValue)
            {
                courtToEdit.SportId = request.SportId.Value;
            }

            _db.SaveChanges();
        }
    }
}
