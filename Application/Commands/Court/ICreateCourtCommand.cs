using Application.DTO.Courts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Court
{
    public interface ICreateCourtCommand : ICommand<CreateCourtDTO>
    {
    }
}
