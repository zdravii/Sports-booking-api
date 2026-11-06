using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CreateReviewDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int CourtId { get; set; }
    }
}
