using Application.DTO.Courts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface ICourtDetailsQuery : IQuery<int,CourtDTO>
    {
    }
}
