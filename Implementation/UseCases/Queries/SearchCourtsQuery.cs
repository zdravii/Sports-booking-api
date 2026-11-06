using Application.DTO.Courts;
using Application.DTO.Pagination;
using Application.Queries;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries
{
    public class SearchCourtsQuery : UseCase, ISearchCourtQuery
    {
        public SearchCourtsQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "search-courts";

        public string Name => "Search courts";

        public PaginationResponse<CourtDTO> Execute(SearchCourtDTO request)
        {
            var query = _db.Courts.AsQueryable();

            if (request.CenterId > 0)
            {
                query = query.Where(c => c.CenterId == request.CenterId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Label))
                query = query.Where(c => c.Label.ToLower().Contains(request.Label.ToLower()));

            if (request.SportId > 0)
                query = query.Where(c => c.SportId == request.SportId);

            int totalCount = query.Count();

            var items = query.OrderBy(c => c.Label)
                             .Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .Select(c => new CourtDTO
                             {
                                 Id = c.Id,
                                 Images = c.Images.Select(i => i.Path),
                                 Label = c.Label,
                                 Center = c.Center.Name,
                                 IsActive = c.isActive,
                                 Sport = c.Sport.Name
                             }).ToList();

            return new PaginationResponse<CourtDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
