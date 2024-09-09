using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Presentation.Extensions;
using BSS.DishDepot.Presentation.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BSS.DishDepot.Presentation.Services
{
    public class ApiResultBuilder : IApiResultBuilder
    {
        private readonly IMapper _mapper;

        public ApiResultBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult CreatedAtResult<TIn, TOut>(Result<TIn> service, TOut response,
            ControllerContext context) where TOut : class
        {
            if (!service.IsSuccessful) return BuildError(service, context);

            _mapper.Map(service.Data, response);
            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Created };
        }

        public IActionResult CreatedAtResult<TIn, TOut>(Result<TIn> service, string route,
            object routeValues, ControllerContext context) where TOut : class
        {
            var response = Activator.CreateInstance<TOut>();
            return CreatedAtResult(service, response, route, routeValues, context);
        }

        public IActionResult CreatedAtResult<TIn, TOut>(Result<TIn> service, TOut response,
            string route, object routeValues, ControllerContext context) where TOut : class
        {
            if (!service.IsSuccessful) return BuildError(service, context);

            _mapper.Map(service.Data, response);
            return new CreatedAtRouteResult(route, routeValues, response);
        }

        public IActionResult OkResult<TIn, TOut>(Result<TIn> service, ControllerContext context)
            where TOut : class
        {
            var response = Activator.CreateInstance<TOut>();
            return OkResult(service, response, context);
        }

        public IActionResult OkResult(Result service, ControllerContext context)
        {
            return !service.IsSuccessful ? BuildError(service, context) : new OkResult();
        }

        public IActionResult OkResult<TIn, TOut>(Result<TIn> service, TOut response,
            ControllerContext context) where TOut : class
        {
            if (!service.IsSuccessful) return BuildError(service, context);

            _mapper.Map(service.Data, response);
            return new OkObjectResult(response);
        }

        private static IActionResult BuildError<TIn>(Result<TIn> result, ActionContext context)
        {
            var error = result.ToProblemDetails();

            return ReturnErrorResult(context, error);
        }

        private static IActionResult BuildError(Result result, ActionContext context)
        {
            var error = result.ToProblemDetails();

            return ReturnErrorResult(context, error);
        }

        private static IActionResult ReturnErrorResult(ActionContext context, ProblemDetails? error)
        {
            var response = new ApiError(error?.Status ?? 400, error?.Detail);
            return new ObjectResult(response) { StatusCode = response.HttpStatusCode };
        }
    }
}
