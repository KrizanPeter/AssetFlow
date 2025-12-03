using AssetFlow.Application.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AssetFlow.API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToApiResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return Results.Ok(new { data = result.Value });

            var error = result.Errors.FirstOrDefault();

            return error switch
            {
                ValidationError => Results.BadRequest(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/400",
                    Title = "Validation failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more validation errors occurred.",
                    Extensions =
                    {
                        ["errors"] = error.Reasons
                            .Select(e => new { property = ((ValidationError)error).Property,  message = e.Message })
                            .ToList()
                    }
                }),

                NotFoundError nf => Results.NotFound(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/404",
                    Title = "Resource not found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = nf.Message
                }),

                BusinessError be => Results.UnprocessableEntity(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/422",
                    Title = "Business rule violation",
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Detail = be.Message
                }),

                UnauthorizedError ue => Results.Json(
                    new ProblemDetails
                    {
                        Type = "https://httpstatuses.com/401",
                        Title = "Unauthorized",
                        Status = StatusCodes.Status401Unauthorized,
                        Detail = ue.Message
                    },
                    statusCode: StatusCodes.Status401Unauthorized
                ),

                ForbiddenError fe => Results.Json(
                    new ProblemDetails
                    {
                        Type = "https://httpstatuses.com/403",
                        Title = "Forbidden",
                        Status = StatusCodes.Status403Forbidden,
                        Detail = fe.Message
                    },
                    statusCode: StatusCodes.Status403Forbidden
                ),

                ConflictError ce => Results.Conflict(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/409",
                    Title = "Conflict",
                    Status = StatusCodes.Status409Conflict,
                    Detail = ce.Message
                }),

                SystemError se => Results.Problem(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/500",
                    Title = "Unexpected error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = se.Message
                }),

                _ => Results.Problem(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/500",
                    Title = "Unhandled error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = string.Join("; ", result.Errors.Select(e => e.Message))
                })
            };
        }
    }
}
