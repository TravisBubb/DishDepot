using BSS.DishDepot.Application.Cqrs.Users;
using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Presentation.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BSS.DishDepot.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApiResultBuilder _builder;

    public UserController(IMediator mediator, IApiResultBuilder builder)
    {
        _mediator = mediator;
        _builder = builder;
    }

    [Authorize]
    [HttpGet("", Name = "GetMyUser")]
    public async Task<IActionResult> GetUser()
    {
        var result = await _mediator.Send(new GetMyUserQuery());
        return _builder.OkResult<Domain.Entities.User, UserResponse>(result, ControllerContext);
    }

    [AllowAnonymous]
    [HttpPost("", Name = "PostUser")]
    public async Task<IActionResult> PostUser([FromBody] PostUserRequest request)
    {
        var result = await _mediator.Send(new PostUserCommand(request));
        var routeValues = new { id = result.Data?.Id };
        return _builder.CreatedAtResult<Domain.Entities.User, UserResponse>(result, "GetMyUser", 
            routeValues, ControllerContext);
    }

    [Authorize]
    [HttpPut("", Name = "PutMyUser")]
    public async Task<IActionResult> PutUser([FromRoute] string id, [FromBody] PutUserRequest request)
    {
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    [HttpPost("authenticate", Name = "AuthenticateUser")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request)
    {
        var result = await _mediator.Send(new AuthenticateUserCommand(request));
        return _builder.OkResult<AccessToken, AccessToken>(result, ControllerContext);
    }
}
