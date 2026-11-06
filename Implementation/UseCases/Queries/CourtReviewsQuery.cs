using Application.DTO;
using Application.DTO.Pagination;
using Application.Queries;
using DataAccess;
using Domain.Entities;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries
{
    public class CourtReviewsQuery : UseCase, ICourtReviewsQuery
    {
        public CourtReviewsQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "get-court-reviews";

        public string Name => "Get court reviews";

        public PaginationResponse<ReviewDTO> Execute(ReviewsRequestDTO request)
        {
            Court? court = _db.Courts.FirstOrDefault(c => c.Id == request.CourtId);

            if (court == null)
                throw new NotFoundException("Court does not exist");

            int totalCount = _db.Reviews.Count(r => r.CourtId == request.CourtId);

            var items = _db.Reviews.Where(r => r.CourtId == request.CourtId)
                                   .OrderByDescending(r => r.Rating)                   
                                   .Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .Select(r => new ReviewDTO
                                   {
                                       Rating = r.Rating,
                                       Comment = r.Comment,
                                       User = r.User.FirstName + " " + r.User.LastName
                                   }).ToList();

            return new PaginationResponse<ReviewDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
