using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Center
{
    public class CenterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> Sports { get; set; }
        public IEnumerable<int> SportIds { get; set; }
    }
}
