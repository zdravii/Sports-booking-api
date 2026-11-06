using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
