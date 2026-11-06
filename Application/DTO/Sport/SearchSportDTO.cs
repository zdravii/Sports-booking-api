using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.DTO.Sport
{
    public class SearchSportDTO : PaginationRequest
    {
        public int? CenterId { get; set; }
    }
}
