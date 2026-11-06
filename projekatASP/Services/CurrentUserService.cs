using Application.Services;
using Application.Exceptions;

namespace projekatASP.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; 
        }
        public int UserId { 
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User.FindFirst("Id")?.Value;

                if (claim == null || !int.TryParse(claim,out int userId))
                {
                    throw new UnauthorizedException("User is not authorized");
                }

                return userId;
            }
        }
        public string Email {
            get 
            {
                var claim = _httpContextAccessor.HttpContext?.User.FindFirst("Email")?.Value;

                if (claim == null)
                {
                    throw new UnauthorizedException("User is not authorized");
                }

                return claim;
            }
        }
    }
}
