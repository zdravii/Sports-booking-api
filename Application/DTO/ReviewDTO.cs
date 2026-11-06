using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ReviewDTO
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
    }
}
