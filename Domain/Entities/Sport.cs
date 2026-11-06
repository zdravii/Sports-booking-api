using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Sport : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<CenterSport> Centers { get; set; } = new List<CenterSport>();
        public virtual IEnumerable<Court> Courts { get; set; } = new List<Court>();
    }
}
