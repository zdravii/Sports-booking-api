using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CenterSport
    {
        public int CenterId { get; set; }
        public int SportId { get; set; }

        public virtual Center Center { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
