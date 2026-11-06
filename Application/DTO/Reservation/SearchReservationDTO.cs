using Application.DTO.Pagination;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservation
{
    public class SearchReservationDTO : PaginationRequest
    {
        public int? UserId { get; set; }
        public DateOnly? Date { get; set; }
        public ReservationStatus? Status { get; set; }
        public int? CourtId { get; set; }
        public int? CenterId { get; set; }
    }
}
