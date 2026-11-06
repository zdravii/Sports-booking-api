using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Pagination
{
    public class PaginationRequest
    {
        private const int MaxPerPage = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPerPage ? MaxPerPage : value < 1 ? 1 : value;
        }
    }
}
