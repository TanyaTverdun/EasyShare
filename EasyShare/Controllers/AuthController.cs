using MediatR;
using Microsoft.AspNetCore.Mvc;
using EasyShare.Application.Features.Auth.Queries.Login;
using EasyShare.Application.Features.Auth.Commands.Register.RegisterUser;
using EasyShare.Application.Features.Auth.Commands.Register.RegisterCompany;

namespace EasyShare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register/user")]
    public async Task<ActionResult<AuthResponseDto>> RegisterUser(
        [FromBody] RegisterUserCommand command)
    {
        var result = await this._mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("register/company")]
    public async Task<ActionResult<AuthResponseDto>> RegisterCompany(
        [FromBody] RegisterCompanyCommand command)
    {
        var result = await this._mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        [FromBody] LoginQuery query)
    {
        var result = await this._mediator.Send(query);

        return Ok(result);
    }
}