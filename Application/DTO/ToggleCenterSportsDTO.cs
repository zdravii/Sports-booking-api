using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ToggleCenterSportsDTO
    {
        public int CenterId { get; set; }
        public List<int> SportIds { get; set; } = new List<int>();
    }
}
