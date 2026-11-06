using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Jwt
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationSeconds { get; set; }
        public int RefreshTokenHours { get; set; }
    }
}
