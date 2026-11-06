using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservation
{
    public class CreateReservationDTO
    {
        public DateTime StartTime { get; set; }
        public int CourtId { get; set; }
    }
}
