using Application.DTO.Courts;
using Application.Queries;
using DataAccess;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries
{
    public class CourtDetailsQuery : UseCase, ICourtDetailsQuery
    {
        public CourtDetailsQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "court-details";

        public string Name => "Details of one court";

        public CourtDTO Execute(int id)
        {
            var court = _db.Courts.FirstOrDefault(c => c.Id == id);

            if (court == null)
            {
                throw new NotFoundException("Court does not exist");
            }

            return new CourtDTO
            {
                Id = court.Id,
                Label = court.Label,
                Sport = court.Sport.Name,
                SportId = court.SportId,
                Center = court.Center.Name,
                CenterId = court.CenterId,
                Images = court.Images.Select(i => i.Path),
                IsActive = court.isActive
            };
        }
    }
}
