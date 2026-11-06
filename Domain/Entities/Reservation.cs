using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Reservation : Entity
    {
        public DateTime StartTime { get; set; }
        public ReservationStatus Status { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CourtId { get; set; }
        public virtual Court Court { get; set; }
    }
}
