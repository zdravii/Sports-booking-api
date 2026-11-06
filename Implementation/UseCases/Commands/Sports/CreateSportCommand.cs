using Application;
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
    public class CreateSportCommand : UseCase, ICreateSportCommand
    {
        private readonly CreateSportValidator _validator;
        public CreateSportCommand(DataBaseContext db,CreateSportValidator validator) : base(db)
        {
            _validator = validator;
        }

        public string Id => "create-sport";

        public string Name => "Create sport";

        public void Execute(CreateSportDTO request)
        {
            _validator.ValidateAndThrow(request);

            if (_db.Sports.Any(s => s.Name.ToLower() == request.Name.ToLower()))
            {
                throw new ConflictException("Sport already exists");
            }

            Domain.Entities.Sport sportToAdd = new Domain.Entities.Sport
            {
                Name = request.Name,
                Description = request.Description
            };

            _db.Sports.Add(sportToAdd);
            _db.SaveChanges();
        }
    }
}
