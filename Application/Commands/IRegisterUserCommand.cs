using Application.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface IRegisterUserCommand : IUseCase
    {
        public int Execute(RegisterUserDTO request);
    }
}
