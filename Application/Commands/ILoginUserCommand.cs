using Application.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface ILoginUserCommand : IUseCase
    {
        public LoginResponseDTO Execute(LoginUserDTO request);
    }
}
