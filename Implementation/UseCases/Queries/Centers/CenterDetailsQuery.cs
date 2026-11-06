using Application.DTO.Center;
using Application.Exceptions;
using Application.Queries;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Queries.Centers
{
    public class CenterDetailsQuery : UseCase, ICenterDetailsQuery
    {
        public CenterDetailsQuery(DataBaseContext db) : base(db)
        {
        }

        public string Id => "get-center-details";

        public string Name => "Get details of one center";

        public CenterDTO Execute(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid parameter");
            }

            Center? center = _db.Centers.FirstOrDefault(c => c.Id == id);

            if (center == null)
            {
                throw new NotFoundException("Selected center does not exists");
            }

            CenterDTO centerDTO = new CenterDTO
            {
                Id = center.Id,
                Name = center.Name,
                Address = center.Address,
                Sports = center.Sports.Select(s => s.Sport.Name).ToList(),
                SportIds = center.Sports.Select(s => s.SportId).ToList()
            };

            return centerDTO;
        }
    }
}
