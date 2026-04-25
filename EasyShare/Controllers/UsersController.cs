using EasyShare.Application.Features.Users.Commands.UpdateProfile;
using EasyShare.Application.Features.Users.Queries.GetProfile;
using EasyShare.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyShare.Controllers
{
    [Authorize(Roles = nameof(AccountType.User))]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileDto>> GetMyProfile()
        {
            var query = new GetUserProfileQuery();
            var result = await this._mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("profile")]
        public async Task<ActionResult<UserProfileResponse>> UpdateMyProfile(
            [FromBody] UpdateUserProfileCommand command,
            CancellationToken cancellationToken)
        {
            var result = await this._mediator.Send(
                command, 
                cancellationToken);

            return Ok(result);
        }
    }
}
