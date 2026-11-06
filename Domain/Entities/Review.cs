using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Review : Entity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CourtId { get; set; }
        public virtual Court Court { get; set; }
    }
}
