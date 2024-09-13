using BSS.DishDepot.Application.Cqrs.Users;
using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Presentation.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BSS.DishDepot.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApiResultBuilder _builder;

    public UserController(IMediator mediator, IApiResultBuilder builder)
    {
        _mediator = mediator;
        _builder = builder;
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> GetUser([FromRoute] string id)
    {
        var result = await _mediator.Send(new GetUserQuery(id));
        return _builder.OkResult<Domain.Entities.User, UserResponse>(result, ControllerContext);
    }

    [HttpPost("", Name = "PostUser")]
    public async Task<IActionResult> PostUser([FromBody] PostUserRequest request)
    {
        var result = await _mediator.Send(new PostUserCommand(request));
        var routeValues = new { id = result.Data?.Id };
        return _builder.CreatedAtResult<Domain.Entities.User, UserResponse>(result, "GetUser", 
            routeValues, ControllerContext);
    }

    [HttpPut("{id}", Name = "PutUser")]
    public async Task<IActionResult> PutUser([FromRoute] string id, [FromBody] PutUserRequest request)
    {
        throw new NotImplementedException();
    }
}
