using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ModularWebApi.Modules.User.Application.Commands;

namespace ModularWebApi.Modules.User.API
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;
        public UserController(IMediator mediator)
        { 
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
        {
            if (registerUserCommand == null)
                throw new ArgumentNullException(nameof(registerUserCommand));

            var userId = await _mediator.Send(registerUserCommand);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            try
            {
                string token = await _mediator.Send(loginUserCommand);

                return Ok(token);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("disable/{userId}")]
        public async Task<IActionResult> Disable(Guid userId)
        {
            try
            {
                var disableUserCommand = new DisableUserCommand
                (
                    userId
                );

                await _mediator.Send(disableUserCommand);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
