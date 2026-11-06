using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Center : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual IEnumerable<CenterSport> Sports { get; set; } = new List<CenterSport>();
        public virtual IEnumerable<Court> Courts { get; set; } = new List<Court>();
    }
}
