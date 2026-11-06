using Application.Commands;
using Application.DTO;
using Application.Services;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Implementation.UseCases.Commands
{
    public class CreateReviewCommand : UseCase, ICreateReviewCommand
    {
        private readonly CreateReviewValidator _validator;
        private readonly ICurrentUserService _currentUserService;
        public CreateReviewCommand(DataBaseContext db,CreateReviewValidator validator,ICurrentUserService currentUserService) : base(db)
        {
            _validator = validator;
            _currentUserService = currentUserService;
        }

        public string Id => "create-review";

        public string Name => "Create review";

        public void Execute(CreateReviewDTO request)
        {
            _validator.ValidateAndThrow(request);

            Domain.Entities.Court? _court = _db.Courts.FirstOrDefault(c => c.Id == request.CourtId);

            if (_court == null)
                throw new NotFoundException("Court you are trying to leave review on does not exist");

            if (!_db.Reservations.Any(r => r.UserId == _currentUserService.UserId && r.Status == Domain.Enums.ReservationStatus.Completed && r.CourtId == request.CourtId))
                throw new ConflictException("You can only review courts you have played at");

            if (_db.Reviews.Any(r => r.UserId == _currentUserService.UserId && r.CourtId == request.CourtId))
                throw new ConflictException("You cannot review same court more than once");

            Review reviewToAdd = new Review
            {
                Rating = request.Rating,
                Comment = request.Comment,
                UserId = _currentUserService.UserId,
                CourtId = request.CourtId
            };

            _db.Reviews.Add(reviewToAdd);
            _db.SaveChanges();
        }       
    }
}
