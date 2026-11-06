using Application.DTO.Center;
using Application.DTO.Pagination;
using Application.Queries;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries.Centers
{
    public class SearchCentersQuery : UseCase, ISearchCentersQuery
    {
        public SearchCentersQuery(DataBaseContext db) : base(db)
        {
            
        }
        public string Id => "search-centers";

        public string Name => "Search centers";

        public PaginationResponse<CenterDTO> Execute(SearchCentersDTO request)
        {
            var query = _db.Centers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(c => EF.Functions.Like(c.Name, $"%{request.Name}%"));

            int totalCount = query.Count();

            var items = query.OrderBy(c => c.Name)
                             .Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .Select(c => new CenterDTO
                             {
                                 Id = c.Id,
                                 Address = c.Address,
                                 Name = c.Name,
                                 Sports = c.Sports.Select(s => s.Sport.Name)
                             }).ToList();

            return new PaginationResponse<CenterDTO> 
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
