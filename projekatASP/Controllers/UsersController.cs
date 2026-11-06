using Application.DTO;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace projekatASP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ISearchUserQuery _searchUserQuery;

        public UsersController(ISearchUserQuery searchUserQuery)
        {
            _searchUserQuery = searchUserQuery;
        }

        [HttpGet]
        public IActionResult SearchUsers([FromQuery] SearchUserDTO request)
        {
            var result = _searchUserQuery.Execute(request);

            return Ok(result);
        }
    }
}
