using Application.Commands.Sport;
using Application.DTO.Sport;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projekatASP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportsController : ControllerBase
    {
        private readonly ICreateSportCommand _createSportCommand;
        private readonly IEditSportCommand _editSportCommand;
        private readonly IDeleteSportCommand _deleteSportCommand;
        private readonly ISearchSportQuery _searchSportQuery;
        private readonly ISportDetailsQuery _sportDetailsQuery;
        public SportsController(ICreateSportCommand createSportCommand, IDeleteSportCommand deleteSportCommand, IEditSportCommand editSportCommand, ISearchSportQuery searchSportQuery,ISportDetailsQuery sportDetailsQuery)
        {
            _createSportCommand = createSportCommand;
            _editSportCommand = editSportCommand;
            _deleteSportCommand = deleteSportCommand;
            _searchSportQuery = searchSportQuery;
            _sportDetailsQuery = sportDetailsQuery;
        }

        [HttpGet("by-center/{centerId}")]
        public IActionResult SearchSports(int centerId, [FromQuery] SearchSportDTO request)
        {
            request.CenterId = centerId;
            var response = _searchSportQuery.Execute(request);

            return Ok(response);
        }

        [HttpGet("{sportId}")]
        public IActionResult SportDetails(int sportId)
        {
            var respones = _sportDetailsQuery.Execute(sportId);

            return Ok(respones);
        }

        [HttpGet]
        public IActionResult GetAllSports([FromQuery] SearchSportDTO request)
        {
            var response = _searchSportQuery.Execute(request);
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateSport([FromBody]CreateSportDTO request)
        {
            _createSportCommand.Execute(request);

            return Created(); //201
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult EditSport([FromBody]EditSportDTO request)
        {
            _editSportCommand.Execute(request);

            return Ok(); //200
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteSport(int id)
        {
            _deleteSportCommand.Execute(id);

            return NoContent(); //204
        }
    }
}
