using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class SearchAuditLogDTO : PaginationRequest
    {
        public string? UserEmail{ get; set; }
        public string? AuditLogName { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
    }
}
