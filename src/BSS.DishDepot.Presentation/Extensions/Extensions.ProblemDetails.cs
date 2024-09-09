using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace BSS.DishDepot.Presentation.Extensions;

public static class ProblemDetailExtensions
{
    public static ProblemDetails? ToProblemDetails<T>(this Result<T> result)
    {
        if (result.IsSuccessful)
            return default;

        var problem = new ProblemDetails();
        problem.Status = result.Kind switch
        {
            ResultKind.Invalid => (int)HttpStatusCode.BadRequest,
            ResultKind.NotFound => (int)HttpStatusCode.NotFound,
            ResultKind.Unauthorized => (int)HttpStatusCode.Unauthorized,
            ResultKind.Unexpected => (int)HttpStatusCode.InternalServerError,
            _ => problem.Status
        };

        problem.Detail = !string.IsNullOrWhiteSpace(result.Message) ? result.Message : result.Kind.ToString();

        if (problem.Status.HasValue)
        {
            var statusCode = problem.Status.GetValueOrDefault(200);
            problem.Type = $"https://httpstatuses.com/{statusCode}";
            problem.Title = ReasonPhrases.GetReasonPhrase(statusCode);
        }

        return problem;
    }

    public static ProblemDetails? ToProblemDetails(this Result result)
    {
        if (result.IsSuccessful)
            return default;

        var problem = new ProblemDetails();
        problem.Status = result.Kind switch
        {
            ResultKind.Invalid => (int)HttpStatusCode.BadRequest,
            ResultKind.NotFound => (int)HttpStatusCode.NotFound,
            ResultKind.Unauthorized => (int)HttpStatusCode.Unauthorized,
            ResultKind.Unexpected => (int)HttpStatusCode.InternalServerError,
            _ => problem.Status
        };

        problem.Detail = !string.IsNullOrWhiteSpace(result.Message) ? result.Message : result.Kind.ToString();

        if (problem.Status.HasValue)
        {
            var statusCode = problem.Status.GetValueOrDefault(200);
            problem.Type = $"https://httpstatuses.com/{statusCode}";
            problem.Title = ReasonPhrases.GetReasonPhrase(statusCode);
        }

        return problem;
    }

    public static ApiError? ToError(this ICollection<ValidationFailure>? failures)
    {
        if (failures is null || failures.Count == 0)
            return default;

        var error = new ApiError
        {
            HttpStatusCode = (int)HttpStatusCode.BadRequest
        };

        var builder = new StringBuilder();

        foreach (var failure in failures)
        {
            builder.Append(failure.ErrorMessage);

            if (failures.Last() != failure)
                builder.Append(", ");
        }

        error.Message = builder.ToString();

        return error;
    }

}