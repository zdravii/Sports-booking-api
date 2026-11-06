using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Pagination
{
    public class PaginationResponse<TDto> where TDto : class
    {
        public IEnumerable<TDto> Items { get; set; } = new List<TDto>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
