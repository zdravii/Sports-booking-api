using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Jwt
{
    public interface IJwtHandler
    {
        JwtResponse MakeToken(User user);
    }
}
