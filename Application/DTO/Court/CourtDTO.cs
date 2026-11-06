using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Courts
{
    public class CourtDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Sport { get; set; }
        public int SportId { get; set; }
        public string Center { get; set; }
        public int CenterId { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
