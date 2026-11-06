using Application.Commands.Center;
using Application.DTO.Center;
using Application.Exceptions;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Centers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Center
{
    public class EditCenterCommand : UseCase, IEditCenterCommand
    {
        private readonly EditCenterValidator _validator;
        public EditCenterCommand(DataBaseContext db,EditCenterValidator validator) : base(db)
        {
            _validator = validator;
        }

        public string Id => "edit-center";

        public string Name => "Edit center";

        public void Execute(EditCenterDTO request)
        {
            _validator.ValidateAndThrow(request);

            Domain.Entities.Center? centerToEdit = _db.Centers.FirstOrDefault(c => c.Id == request.CenterId);

            if (centerToEdit == null)
            {
                throw new NotFoundException("Center you trying to edit does not exist");
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                if (_db.Centers.Any(c => c.Name.ToLower() == request.Name.ToLower() && c.Id != request.CenterId))
                {
                    throw new ConflictException("Name already exist");
                }

                centerToEdit.Name = request.Name.Trim();
            }

            if (!string.IsNullOrWhiteSpace(request.Address))
            {
                if (_db.Centers.Any(c => c.Address.ToLower() == request.Address.ToLower() && c.Id != request.CenterId))
                {
                    throw new ConflictException("Cneter already exist on provided address");
                }

                centerToEdit.Address = request.Address.Trim();
            }

            _db.SaveChanges();
        }
    }
}
