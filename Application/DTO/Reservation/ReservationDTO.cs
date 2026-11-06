using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Reservation
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public ReservationStatus Status { get; set; }
        public string Court{ get; set; }
        public string Center { get; set; }
        public string User { get; set; }
    }
}
