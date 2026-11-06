using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
    }
}
