using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Sport
{
    public class EditSportDTO
    {
        public int SportId { get; set; }
        public string ?Name { get; set; }
        public string ?Description { get; set; }
    }
}
