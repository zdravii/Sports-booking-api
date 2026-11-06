using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Courts
{
    public class CreateCourtDTO
    {
        public string Label { get; set; }
        public int SportId { get; set; }
        public int CenterId { get; set; }
    }
}
