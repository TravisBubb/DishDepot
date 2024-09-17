using BSS.DishDepot.Application.Cqrs.Recipes;
using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Presentation.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipe = BSS.DishDepot.Domain.Entities.Recipe;

namespace BSS.DishDepot.Api.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApiResultBuilder _builder;

        public RecipeController(IMediator mediator, IApiResultBuilder builder)
        {
            _mediator = mediator;
            _builder = builder;
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetRecipeQuery(id));
            return _builder.OkResult<Recipe, RecipeResponse>(result, ControllerContext);
        }

        [Authorize]
        [HttpGet("", Name = "GetRecipes")]
        public async Task<IActionResult> GetRecipes()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost("", Name = "PostRecipe")]
        public async Task<IActionResult> PostRecipe([FromBody] PostRecipeRequest request)
        {
            var result = await _mediator.Send(new PostRecipeCommand(request));
            var routeValues = new { id = result.Data?.Id };
            return _builder.CreatedAtResult<Recipe, RecipeResponse>(result, "GetRecipe",
                routeValues, ControllerContext);
        }

        //[Authorize]
        //[HttpPut("{id}", Name = "PutRecipe")]
        //public async Task<IActionResult> PutRecipe([FromRoute] string id, [FromBody] PutRecipeRequest request)
        //{
        //    return Ok();
        //}
    }
}
