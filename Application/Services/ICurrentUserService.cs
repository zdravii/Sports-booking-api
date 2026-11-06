using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface ICurrentUserService
    {
        public int UserId { get; }
        public string Email { get; }
    }
}
