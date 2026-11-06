using Application.DTO;
using Application.DTO.Pagination;
using Application.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries
{
    public class SearchAuditLogQuery : UseCase, ISearchAuditLogQuery
    {
        public SearchAuditLogQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "search-audit-log";

        public string Name => "Search audit log";

        public PaginationResponse<AuditLogDTO> Execute(SearchAuditLogDTO request)
        {
            var query = _db.AuditLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.AuditLogName))
                query = query.Where(al => al.Action.ToLower().Contains(request.AuditLogName.ToLower()));

            if (!string.IsNullOrWhiteSpace(request.UserEmail))
                query = query.Where(al => al.User.Email.ToLower().Contains(request.UserEmail.ToLower()));

            if (request.DateFrom.HasValue)
            {
                DateTime fromDateTime = request.DateFrom.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where(al => al.CreatedAt >= fromDateTime);
            }

            if (request.DateTo.HasValue)
            {
                DateTime toDateTime = request.DateTo.Value.ToDateTime(TimeOnly.MinValue).AddDays(1);
                query = query.Where(al => al.CreatedAt < toDateTime);
            }

            int totalCount = query.Count();

            var items = query.OrderBy(al => al.CreatedAt)
                             .Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .Select(al => new AuditLogDTO
                             {
                                 AuditLogName = al.Action,
                                 Description = al.Description,
                                 DateAndTime = al.CreatedAt,
                                 User = al.User.Email
                             }).ToList();

            return new PaginationResponse<AuditLogDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
