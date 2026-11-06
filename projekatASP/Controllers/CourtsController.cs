using Application.Commands;
using Application.Commands.Court;
using Application.DTO;
using Application.DTO.Court;
using Application.DTO.Courts;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projekatASP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourtsController : ControllerBase
    {
        private readonly ICreateCourtCommand _createCourtCommand;
        private readonly IEditCourtCommand _editCourtCommand;
        private readonly IDeleteCourtCommand _deleteCourtCommand;
        private readonly ICreateReviewCommand _createReviewCommand;
        private readonly ICourtReviewsQuery _courtReviewsQuery;
        private readonly IUploadCourtImageCommand _uploadCourtImageCommand;
        private readonly ISearchCourtQuery _searchCourtQuery;
        private readonly ICourtDetailsQuery _courtDetailsQuery;

        public CourtsController(ICreateCourtCommand createCourtCommand, IDeleteCourtCommand deleteCourtCommand,IEditCourtCommand editCourtCommand,ICreateReviewCommand createReviewCommand,ICourtReviewsQuery courtReviewsQuery,IUploadCourtImageCommand uploadCourtImageCommand,ISearchCourtQuery searchCourtQuery,ICourtDetailsQuery courtDetailsQuery)
        {
            _createCourtCommand = createCourtCommand;
            _editCourtCommand = editCourtCommand;
            _deleteCourtCommand = deleteCourtCommand;
            _createReviewCommand = createReviewCommand;
            _courtReviewsQuery = courtReviewsQuery;
            _uploadCourtImageCommand = uploadCourtImageCommand;
            _searchCourtQuery = searchCourtQuery;
            _courtDetailsQuery = courtDetailsQuery;
        }

        [HttpGet]
        public IActionResult GetAllCourts([FromQuery]SearchCourtDTO request)
        {
            var response = _searchCourtQuery.Execute(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourtDetails(int id)
        {
            var response = _courtDetailsQuery.Execute(id);

            return Ok(response);
        }

        [HttpGet("{courtId}/reviews")]
        public IActionResult GetCourtReviews(int courtId,[FromBody] ReviewsRequestDTO request)
        {
            request.CourtId = courtId;
            var response = _courtReviewsQuery.Execute(request);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateCourt([FromBody]CreateCourtDTO request)
        {
            _createCourtCommand.Execute(request);

            return Created(); // 201
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{courId}/image")]
        public IActionResult UploadCourtImage(int courId, [FromForm] IFormFile file, [FromForm] string? alt)
        {
            UploadCourtImageDTO request = new UploadCourtImageDTO
            {
                File = file,
                Alt = alt,
                CourtId = courId
            };

            _uploadCourtImageCommand.Execute(request);

            return Created();
        }

        [Authorize]
        [HttpPost("{courId}/review")]
        public IActionResult CreateReview(int courId, [FromBody] CreateReviewDTO request)
        {
            request.CourtId = courId;
            _createReviewCommand.Execute(request);

            return Created(); //201
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult EditCourt([FromBody]EditCourtDTO request)
        {
            _editCourtCommand.Execute(request);

            return NoContent(); // 204
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCourt(int id)
        {
            _deleteCourtCommand.Execute(id);

            return NoContent(); // 204
        }
    }
}
