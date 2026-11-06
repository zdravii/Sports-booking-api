using Application;
using Application.DTO;
using Application.DTO.Pagination;
using Application.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries
{
    public class SearchUserQuery : UseCase, ISearchUserQuery
    {
        public SearchUserQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "search-user";

        public string Name => "Search user";

        public PaginationResponse<UserDTO> Execute(SearchUserDTO request)
        {
            var query = _db.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Email))
                query = query.Where(u => u.Email.ToLower().Trim().Contains(request.Email.ToLower().Trim()));

            if (request.RoleId.HasValue)
                query = query.Where(u => u.RoleId == request.RoleId.Value);

            if (request.isActive.HasValue)
                query = query.Where(u => u.IsActive == request.isActive.Value);

            int totalCount = query.Count();

            var items = query.OrderBy(u => u.FirstName)
                             .Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .Select(u => new UserDTO
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 isActive = u.IsActive,
                                 Role = u.Role.Name
                             }).ToList();

            return new PaginationResponse<UserDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber
            };
        }
    }
}
