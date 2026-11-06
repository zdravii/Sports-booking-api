using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class SearchUserDTO : PaginationRequest
    {
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public bool? isActive { get; set; }
    }
}
