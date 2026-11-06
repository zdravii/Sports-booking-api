using Application.DTO.Pagination;
using Application.DTO.Reservation;
using Application.Exceptions;
using Application.Queries;
using Application.Services;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Implementation.UseCases.Queries.Reservations
{
    public class UserReservationQuery : UseCase, IUserReservationsQuery
    {
        private readonly ICurrentUserService _currentUserService;
        public UserReservationQuery(DataBaseContext db,ICurrentUserService currentUserService) : base(db)
        {
            _currentUserService = currentUserService;
        }

        public string Id => "get-user-reservation";

        public string Name => "Show all reservation for one user";

        public PaginationResponse<ReservationDTO> Execute(PaginationRequest request)
        {
            User? user = _db.Users.FirstOrDefault(u => u.Id == _currentUserService.UserId);

            if (user == null)
                throw new NotFoundException("User does not exist");

            int totalCount = _db.Reservations.Where(r => r.UserId == user.Id).Count();

            var items = _db.Reservations.OrderBy(r=>r.StartTime)
                                        .Skip((request.PageNumber - 1) * request.PageSize)
                                        .Take(request.PageSize)
                                        .Where(r => r.UserId == user.Id)
                                        .Select(x => new ReservationDTO
                                            {
                                                Id = x.Id,
                                                StartTime = x.StartTime,
                                                Status = x.Status,
                                                Court = x.Court.Label,
                                                Center = x.Court.Center.Name
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
