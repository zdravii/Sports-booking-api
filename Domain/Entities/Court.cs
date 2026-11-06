using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Court : Entity
    {
        public string Label { get; set; }
        public bool isActive { get; set; }
        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }
        public int CenterId { get; set; }
        public virtual Center Center { get; set; }
        public virtual IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
        public virtual IEnumerable<Review> Reviews { get; set; } = new List<Review>();
        public virtual IEnumerable<CourtImage> Images { get; set; } = new List<CourtImage>();
    }
}
