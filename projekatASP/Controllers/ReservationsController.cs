using Application.Commands.Reservation;
using Application.DTO.Pagination;
using Application.DTO.Reservation;
using Application.Queries;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projekatASP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ICreateReservationCommand _createReservationCommand;
        private readonly IDeleteReservationCommand _deleteReservationCommand;
        private readonly IUserReservationsQuery _userReservationsQuery;
        private readonly ISearchReservationsQuery _searchReservationsQuery;
        public ReservationsController(ICreateReservationCommand createReservationCommand, IDeleteReservationCommand deleteReservationCommand,IUserReservationsQuery userReservationsQuery,ISearchReservationsQuery searchReservationsQuery)
        {
            _createReservationCommand = createReservationCommand;
            _deleteReservationCommand = deleteReservationCommand;
            _userReservationsQuery = userReservationsQuery;
            _searchReservationsQuery = searchReservationsQuery;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateReservation([FromBody] CreateReservationDTO request)
        {
            _createReservationCommand.Execute(request);

            return Created(); //201
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            _deleteReservationCommand.Execute(id);

            return NoContent(); //204
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult GetCurrrentUserReservations([FromQuery]PaginationRequest request)
        {
            var result = _userReservationsQuery.Execute(request);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public IActionResult SearchReservation([FromQuery]SearchReservationDTO request)
        {
            var result = _searchReservationsQuery.Execute(request);

            return Ok(result);
        }
    }
}
