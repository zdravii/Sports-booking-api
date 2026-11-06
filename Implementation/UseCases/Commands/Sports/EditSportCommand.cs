using Application.Commands.Sport;
using Application.DTO.Sport;
using Application.Exceptions;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Sports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Sport
{
    public class EditSportCommand : UseCase, IEditSportCommand
    {
        private readonly EditSportValidator _validator;
        public EditSportCommand(DataBaseContext db,EditSportValidator validator) : base(db)
        {
            _validator = validator;
        }

        public string Id => "edit-sport";

        public string Name => "Edit sport";

        public void Execute(EditSportDTO request)
        {
            _validator.ValidateAndThrow(request);

            Domain.Entities.Sport? sportToEdit = _db.Sports.FirstOrDefault(s => s.Id == request.SportId);

            if (sportToEdit == null)
            {
                throw new NotFoundException("Sport you are trying to edit does not exist");
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                if (_db.Sports.Any(s => s.Id != request.SportId && s.Name.ToLower() == request.Name.ToLower()))
                {
                    throw new ConflictException("Sport already exist");
                }

                sportToEdit.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                sportToEdit.Description = request.Description;
            }

            _db.SaveChanges();
        }
    }
}
