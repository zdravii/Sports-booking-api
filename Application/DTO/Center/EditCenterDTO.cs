using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Center
{
    public class EditCenterDTO
    {
        public int CenterId { get; set; }
        public string ?Name { get; set; }
        public string ?Address { get; set; }
    }
}
