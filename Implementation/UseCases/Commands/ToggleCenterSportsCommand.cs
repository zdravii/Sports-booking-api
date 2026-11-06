using Application.Commands;
using Application.DTO;
using DataAccess;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands
{
    public class ToggleCenterSportsCommand : UseCase, IToggleCenterSportsCommand
    {
        private readonly ToggleCenterSportsValidator _validator;
        public ToggleCenterSportsCommand(DataBaseContext db,ToggleCenterSportsValidator validator) : base(db)
        {
            _validator = validator;
        }

        public string Id => "toggle-center-sports";

        public string Name => "Center and sports assignment";

        public void Execute(ToggleCenterSportsDTO request)
        {
            _validator.ValidateAndThrow(request);

            Domain.Entities.Center? center = _db.Centers.FirstOrDefault(c => c.Id == request.CenterId);

            if (center == null)
                throw new NotFoundException("Center does not exist");

            var selectedSportsId = _db.Sports.Where(s => request.SportIds.Contains(s.Id))
                                             .Select(s => s.Id)
                                             .ToHashSet();

            if (selectedSportsId.Count != request.SportIds.Distinct().Count())
                throw new NotFoundException("One or more selected sports does not exist");

            var currentlyLinkedSports = _db.CenterSports.Where(cs => cs.CenterId == center.Id).ToList();

            var toRemove = currentlyLinkedSports.Where(cls => !selectedSportsId.Contains(cls.SportId)).ToList();
            var sportsInUse = _db.Courts
                                 .Where(c => c.CenterId == center.Id)
                                 .Where(c => toRemove.Select(x => x.SportId).Contains(c.SportId))
                                 .Select(c => c.SportId)
                                 .Distinct()
                                 .ToList();

            if (sportsInUse.Any())
                throw new ConflictException("Cannot remove sport because the center has courts for this sport");

            _db.CenterSports.RemoveRange(toRemove);

            var currenltyLinkedSportsIds = currentlyLinkedSports.Select(cls => cls.SportId).ToHashSet();
            var toAdd = selectedSportsId.Where(id => !currenltyLinkedSportsIds.Contains(id)).Select(id => new CenterSport
            {
                CenterId = center.Id,
                SportId = id
            }).ToList();
            _db.CenterSports.AddRange(toAdd);

            _db.SaveChanges();
        }
    }
}
