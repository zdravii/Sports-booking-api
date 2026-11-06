using Application.DTO.Center;
using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ISearchCentersQuery : IQuery<SearchCentersDTO,PaginationResponse<CenterDTO>>
    {
    }
}
