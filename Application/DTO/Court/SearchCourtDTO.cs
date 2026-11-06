using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Courts
{
    public class SearchCourtDTO : PaginationRequest
    {
        public int? CenterId { get; set; }
        public string? Label { get; set; }
        public int? SportId { get; set; }
    }
}
