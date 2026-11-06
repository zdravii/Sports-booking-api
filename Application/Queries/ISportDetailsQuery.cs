using Application.DTO.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ISportDetailsQuery : IQuery<int,SportDTO>
    {
    }
}
