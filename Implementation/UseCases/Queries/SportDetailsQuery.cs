using Application.DTO.Sport;
using Application.Queries;
using DataAccess;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries
{
    public class SportDetailsQuery : UseCase, ISportDetailsQuery
    {
        public SportDetailsQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "sport-details";

        public string Name => "Details fo one sport";

        public SportDTO Execute(int id)
        {
            var sport = _db.Sports.FirstOrDefault(s => s.Id == id);

            if (sport == null)
            {
                throw new NotFoundException("Sport does not exist");
            }

            return new SportDTO 
            {
                Id = sport.Id,
                Description = sport.Description,
                Name = sport.Name
            };
        }
    }
}
