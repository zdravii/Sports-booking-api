using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CourtImage : Entity
    {
        public string Path { get; set; }
        public string Alt { get; set; }
        public int CourtId { get; set; }
        public virtual Court Court { get; set; }
    }
}
