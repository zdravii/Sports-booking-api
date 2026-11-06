using Application.DTO;
using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ISearchUserQuery : IQuery<SearchUserDTO,PaginationResponse<UserDTO>>
    {
    }
}
