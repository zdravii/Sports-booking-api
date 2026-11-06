using Application.Commands.Center;
using Application.DTO.Center;
using Application.Exceptions;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Centers;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Centers
{
    public class CreateCenterCommand : UseCase, ICreateCenterCommand
    {
        private readonly CreateCenterValidator _validator;
        public CreateCenterCommand(DataBaseContext db,CreateCenterValidator validator) : base(db)
        {
            _validator = validator;
        }

        public string Id => "create-center";

        public string Name => "Create center";

        public void Execute(CreateCenterDTO request)
        {
            _validator.ValidateAndThrow(request);

            if (_db.Centers.Any(c => c.Name == request.Name))
            {
                throw new ConflictException("Center with same name already exists.");
            }

            if (_db.Centers.Any(c => c.Address == request.Address))
            {
                throw new ConflictException("Center on same address already exists.");
            }

            Domain.Entities.Center centerToAdd = new Domain.Entities.Center
            {
                Name = request.Name,
                Address = request.Address
            };

            _db.Centers.Add(centerToAdd);
            _db.SaveChanges();
        }
    }
}
