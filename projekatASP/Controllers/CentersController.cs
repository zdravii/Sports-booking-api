using Application.Commands;
using Application.Commands.Center;
using Application.DTO;
using Application.DTO.Center;
using Application.DTO.Courts;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projekatASP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CentersController : ControllerBase
    {
        private readonly ISearchCentersQuery _searchCentersQuery;
        private readonly ICreateCenterCommand _createCenterCommand;
        private readonly ICenterDetailsQuery _centerDetailsQuery;
        private readonly IDeleteCenterCommand _deleteCenterCommand;
        private readonly IEditCenterCommand _editCenterCommand;
        private readonly ISearchCourtQuery _searchCourtsQuery;
        private readonly IToggleCenterSportsCommand _toggleCenterSportsCommand;

        public CentersController(ISearchCentersQuery searchCentersQuery, ICreateCenterCommand createCenterCommand, ICenterDetailsQuery centerDetailsQuery, IDeleteCenterCommand deleteCenterCommand,IEditCenterCommand editCenterCommand, ISearchCourtQuery searchCourtsQuery,IToggleCenterSportsCommand toggleCenterSportsCommand)
        {
            _searchCentersQuery = searchCentersQuery;
            _createCenterCommand = createCenterCommand;
            _centerDetailsQuery = centerDetailsQuery;
            _deleteCenterCommand = deleteCenterCommand;
            _editCenterCommand = editCenterCommand;
            _toggleCenterSportsCommand = toggleCenterSportsCommand;

            _searchCourtsQuery = searchCourtsQuery;
        }

        [HttpGet]
        public IActionResult SearchCenters([FromQuery] SearchCentersDTO request)
        {
            var response = _searchCentersQuery.Execute(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetCenterDetails(int id)
        {
            CenterDTO response = _centerDetailsQuery.Execute(id);

            return Ok(response);
        }

        [HttpGet("{id}/courts")]
        public IActionResult SearchCourts(int id, [FromQuery] SearchCourtDTO request)
        {
            request.CenterId = id;

            var response =_searchCourtsQuery.Execute(request);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateCenter([FromBody]CreateCenterDTO request)
        {
            _createCenterCommand.Execute(request);

            return Created(); // 201
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult EditCenter([FromBody] EditCenterDTO request) 
        {
            _editCenterCommand.Execute(request);

            return Ok(); // 200
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{centerId}/sports")]
        public IActionResult ToggleCenterSports(int centerId, [FromBody] List<int> sportIds)
        {
            ToggleCenterSportsDTO request = new ToggleCenterSportsDTO
            {
                CenterId = centerId,
                SportIds = sportIds
            };

            _toggleCenterSportsCommand.Execute(request);

            return NoContent(); // 204
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCenter(int id)
        {
            _deleteCenterCommand.Execute(id);

            return NoContent(); // 204
        }
    }
}
