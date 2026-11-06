using Application.DTO.Pagination;
using Application.DTO.Reservation;
using Application.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries.Reservations
{
    public class SearchReservationQuery : UseCase, ISearchReservationsQuery
    {
        public SearchReservationQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "search-reservations";

        public string Name => "Search reservations";

        public PaginationResponse<ReservationDTO> Execute(SearchReservationDTO request)
        {
            var query = _db.Reservations.AsQueryable();

            if (request.UserId.HasValue)
            {
                query = query.Where(r => r.UserId == request.UserId.Value);
            }

            if (request.Date.HasValue)
            {
                DateTime startOfDay = request.Date.Value.ToDateTime(TimeOnly.MinValue);
                DateTime endOfDay = startOfDay.AddDays(1);

                query = query.Where(r => r.StartTime >= startOfDay && r.StartTime < endOfDay);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(r => r.Status == request.Status.Value);
            }

            if (request.CourtId.HasValue)
            {
                query = query.Where(r => r.CourtId == request.CourtId);
            }

            if (request.CenterId.HasValue)
            {
                query = query.Where(r => r.Court.CenterId == request.CenterId);
            }

            int totalCount = query.Count();

            var items = query.OrderBy(r => r.StartTime)
                             .Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .Select(r => new ReservationDTO
                             {
                                 Id = r.Id,
                                 StartTime = r.StartTime,
                                 Center = r.Court.Center.Name,
                                 Court = r.Court.Label,
                                 Status = r.Status,
                                 User = r.User.FirstName + " " + r.User.LastName
                             }).ToList();

            return new PaginationResponse<ReservationDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
