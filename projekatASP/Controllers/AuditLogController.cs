using Application.DTO;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace projekatASP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly ISearchAuditLogQuery _searchAuditLogQuery;
        public AuditLogController(ISearchAuditLogQuery searchAuditLogQuery)
        {
            _searchAuditLogQuery = searchAuditLogQuery;
        }

        [HttpGet]
        public IActionResult SearchAuditLog([FromQuery]SearchAuditLogDTO request)
        {
            var response = _searchAuditLogQuery.Execute(request);

            return Ok(response);
        }
    }
}
