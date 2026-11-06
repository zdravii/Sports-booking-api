using Application.DTO.Pagination;
using Application.DTO.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IUserReservationsQuery : IQuery<PaginationRequest, PaginationResponse<ReservationDTO>>
    {
    }
}
