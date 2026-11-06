using Application.DTO.Pagination;
using Application.DTO.Sport;
using Application.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries
{
    public class SearchSportQuery : UseCase, ISearchSportQuery
    {
        public SearchSportQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "search-sport";

        public string Name => "Search sport";

        public PaginationResponse<SportDTO> Execute(SearchSportDTO request)
        {
            var query = _db.Sports.AsQueryable();

            if (request.CenterId.HasValue && request.CenterId > 0)
                query = query.Where(s => s.Centers.Any(c => c.CenterId == request.CenterId));

            int totalCount = query.Count();

            var items = query.OrderBy(s => s.Name)
                             .Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .Select(s => new SportDTO
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Description = s.Description
                             }).ToList();

            return new PaginationResponse<SportDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
