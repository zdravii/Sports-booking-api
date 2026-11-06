using Application.DTO.Pagination;
using Application.DTO.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ISearchSportQuery : IQuery<SearchSportDTO, PaginationResponse<SportDTO>>
    {
    }
}
