using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ReviewsRequestDTO : PaginationRequest
    {
        public int CourtId { get; set; }
    }
}
