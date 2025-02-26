using IssueManagement.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace IssueManagement.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred.",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.Message, 
                Instance = context.TraceIdentifier,
            };

            switch (exception)
            {
                case UnauthorizedAccessException:
                    problemDetails.Title = "Unauthorized access.";
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Type = nameof(UnauthorizedAccessException);
                    break;

                case AuthException:
                    problemDetails.Title = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Type = nameof(AuthException);
                    break;

                case ChangePasswordException ex:
                    problemDetails.Title = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Type = nameof(ChangePasswordException);
                    
                    break;

                case ArgumentNullException:
                case ArgumentException:
                    problemDetails.Title = "Invalid request data.";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(ArgumentException);
                    break;

                case KeyNotFoundException:
                    problemDetails.Title = "Resource not found.";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Type = nameof(KeyNotFoundException);
                    break;

                case EmailAlreadyExistsException:
                    problemDetails.Title = "Email already exists.";
                    problemDetails.Status = (int)HttpStatusCode.Conflict;
                    problemDetails.Type = nameof(EmailAlreadyExistsException);
                    break;

                case ActionNotAllowedException:
                    problemDetails.Title = "Action not allowed to perform.";
                    problemDetails.Status = (int)HttpStatusCode.Forbidden;
                    problemDetails.Type = nameof(ActionNotAllowedException);
                    break;

                case InvalidPasswordException:
                    problemDetails.Title = "Incorrect password.";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(InvalidPasswordException);
                    break;

                case ConfirmationException:
                    problemDetails.Title = "Error occured while confirming email";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(ConfirmationException);
                    break;

                case IssueNonExistentException:
                    problemDetails.Title = "Issue does not exist.";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Type = nameof(IssueNonExistentException);
                    break;

                case StatusException:
                    problemDetails.Title = "Status could not be set";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(StatusException);
                    break;

                case UsernameAlreadyExistsException:
                    problemDetails.Title = "User already exists.";
                    problemDetails.Status = (int)HttpStatusCode.Conflict;
                    problemDetails.Type = nameof(UsernameAlreadyExistsException);
                    break;

                case UserNotFoundException:
                    problemDetails.Title = "User does not exist";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Type = nameof(UserNotFoundException);
                    break;

                case ConstraintException:
                    problemDetails.Title = "Key constrint error occured";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(ConstraintException);
                    break;

                case RegistrationException:
                    problemDetails.Title = "Error while registering";
                    problemDetails.Status = (int)HttpStatusCode.Forbidden;
                    problemDetails.Type = nameof(RegistrationException);
                    break;

            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status.Value;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var result = JsonSerializer.Serialize(problemDetails, options);
            return context.Response.WriteAsync(result);
        }
    }

}

