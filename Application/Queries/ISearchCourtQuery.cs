using Application.DTO.Courts;
using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ISearchCourtQuery : IQuery<SearchCourtDTO,PaginationResponse<CourtDTO>>
    {
    }
}
