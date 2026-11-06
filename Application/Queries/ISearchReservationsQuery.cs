using Application.DTO.Pagination;
using Application.DTO.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ISearchReservationsQuery : IQuery<SearchReservationDTO,PaginationResponse<ReservationDTO>>
    {
    }
}
