using BSS.DishDepot.Domain.Foundation;
using Microsoft.AspNetCore.Mvc;

namespace BSS.DishDepot.Presentation.Interfaces
{
    public interface IApiResultBuilder
    {
        IActionResult OkResult<TIn, TOut>(Result<TIn> service, ControllerContext context)
            where TOut : class;

        IActionResult OkResult(Result service, ControllerContext context);

        IActionResult OkResult<TIn, TOut>(Result<TIn> service, TOut response, ControllerContext context)
            where TOut : class;

        IActionResult CreatedAtResult<TIn, TOut>(Result<TIn> service, TOut response, ControllerContext context)
            where TOut : class;

        IActionResult CreatedAtResult<TIn, TOut>(Result<TIn> service, string route, object routeValues,
            ControllerContext context) where TOut : class;

        IActionResult CreatedAtResult<TIn, TOut>(Result<TIn> service, TOut response, string route, object routeValues,
            ControllerContext context) where TOut : class;
    }
}
