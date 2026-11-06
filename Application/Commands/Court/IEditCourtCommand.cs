using Application.DTO.Court;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Court
{
    public interface IEditCourtCommand : ICommand<EditCourtDTO>
    {
    }
}
