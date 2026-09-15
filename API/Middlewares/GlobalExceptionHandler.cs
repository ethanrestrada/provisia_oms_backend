using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API.Middlewares
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) =>
            _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context,
            Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Uncontrolled exception {Message}", exception.Message);

            if (exception is FluentValidation.ValidationException validationException) {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                await context.Response.WriteAsJsonAsync(
                    new ValidationProblemDetails(errors)
                    {
                        Title = "Validation errors",
                        Status = StatusCodes.Status400BadRequest,
                    },
                    cancellationToken
                );

                return true;
            }

            var response = exception switch
            {
                NotFoundException e => (StatusCodes.Status404NotFound, "Resource not found", e.Message),
                ConflictException e => (StatusCodes.Status409Conflict, "Conflict", e.Message),
                UnauthorizedException e => (StatusCodes.Status401Unauthorized, "Unauthorized", e.Message),
                ForbiddenException e => (StatusCodes.Status403Forbidden, "Forbidden", e.Message),
                BusinessException e => (StatusCodes.Status422UnprocessableEntity, "Business rule violation", e.Message),
                DbUpdateException { InnerException: PostgresException pgEx } => MapPostgreError(pgEx),
                PostgresException pgEx => MapPostgreError(pgEx),
                _ => (StatusCodes.Status500InternalServerError, "Internal server error", "An unexpected error occurred.")
            };

            context.Response.StatusCode = response.Item1;

            await context.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Title = response.Item2,
                    Detail = response.Item3,
                    Status = response.Item1
                },
                cancellationToken
            );

            return true;
        }

        private static (int statucCode, string title, string detail) MapPostgreError(PostgresException ex)
        {
            return ex.SqlState switch
            {
                PostgresErrorCodes.UniqueViolation => (StatusCodes.Status409Conflict, "Record already exists", "A record with the provided information already exists."),
                PostgresErrorCodes.ForeignKeyViolation => (StatusCodes.Status409Conflict, "Invalid reference", "The operation cannot be completed because a related record exists."),
                PostgresErrorCodes.NotNullViolation => (StatusCodes.Status400BadRequest, "Required field", "Required information is missing."),
                PostgresErrorCodes.CheckViolation => (StatusCodes.Status422UnprocessableEntity, "Invalid data", "The provided data does not meet the required constraints."),
                PostgresErrorCodes.StringDataRightTruncation => (StatusCodes.Status400BadRequest, "Data too long", "One of the provided values exceeds the allowed length."),
                _ => (StatusCodes.Status500InternalServerError, "Internal server error", "An unexpected error occurred.")
            };
        }
    }
}
