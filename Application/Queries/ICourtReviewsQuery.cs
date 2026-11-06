using Application.DTO;
using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ICourtReviewsQuery : IQuery<ReviewsRequestDTO,PaginationResponse<ReviewDTO>>
    {
    }
}
