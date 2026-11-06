using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Court
{
    public class EditCourtDTO
    {
        public int CourtId { get; set; }
        public string? Label { get; set; }
        public bool? isActive { get; set; }
        public int? SportId { get; set; }
        public int? CenterId { get; set; }
    }
}
