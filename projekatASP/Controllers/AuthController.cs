using Application.Commands;
using Application.DTO.Auth;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace projekatASP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUserCommand _registerUserCommand;
        private readonly ILoginUserCommand _loginCommand;

        public AuthController(IRegisterUserCommand registerUserCommand,ILoginUserCommand loginCommand)
        {
            _registerUserCommand = registerUserCommand;
            _loginCommand = loginCommand;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO request) 
        {
            _registerUserCommand.Execute(request);

            return Created(); //201
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDTO request)
        {
            LoginResponseDTO response = _loginCommand.Execute(request);

            return Ok(response);
        }
    }
}
