using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Center
{
    public class SearchCentersDTO : PaginationRequest
    {
        public string ?Name { get; set; }
    }
}
