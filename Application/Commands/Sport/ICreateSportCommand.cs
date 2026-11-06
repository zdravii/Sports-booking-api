using Application.DTO.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Sport
{
    public interface ICreateSportCommand : ICommand<CreateSportDTO>
    {
    }
}
