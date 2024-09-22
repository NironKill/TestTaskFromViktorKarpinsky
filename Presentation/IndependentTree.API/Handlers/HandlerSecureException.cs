using FluentValidation;
using FluentValidation.Results;
using IndependentTree.Application.Common.Exceptions;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Models;
using IndependentTree.Application.Repositories.Magazine;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace IndependentTree.API.Handlers
{
    public class HandlerSecureException : IExceptionHandler
    {
        private readonly ILogger<HandlerSecureException> _logger;
        private readonly IServiceProvider _serviceProvider;

        public HandlerSecureException(ILogger<HandlerSecureException> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            string responce = string.Empty;
            string type = string.Empty;
            string message = string.Empty;
            string bodyParameters = string.Empty;

            Guid exceptionId = Guid.NewGuid();
            int timeStamp = Convert.ToInt32(DateTime.UtcNow.AddDays(30).Subtract(DateTime.UnixEpoch).TotalSeconds);

            if (exception is ValidationException fluentException)
            {
                ValidationFailure error = fluentException.Errors.FirstOrDefault();
                switch (int.Parse(error.ErrorCode))
                {
                    case (int)StatuseCode.NotFound:
                        message = error.ErrorMessage;
                        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        type = "NotFound";
                        break;

                    case (int)StatuseCode.BadRequest:
                        message = error.ErrorMessage;
                        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        type = "BadRequest";
                        break;

                    default:
                        message = error.ErrorMessage;
                        type = "Exception";
                        break;
                }

                responce = JsonSerializer.Serialize(new
                {
                    type = type,
                    id = exceptionId,
                    data = new { message = $"{message} ID={exceptionId}" }
                });

                bodyParameters = error.CustomState.ToString();

                _logger.LogError("type:{Type}, id:{ExceptionId}, data:[message:{Detail} ID={ExceptionId}]", type, exceptionId, message, exceptionId);
            }

            else if (exception is SecureException secureException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                type = "Secure";

                responce = JsonSerializer.Serialize(new
                {
                    type = type,
                    id = exceptionId,
                    data = new { message = exception.Message }
                });

                _logger.LogError("type:{Type}, id:{ExceptionId}, data:[message:{Detail}]", type, exceptionId, exception.Message);
            }

            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                type = "Exception";
                responce = JsonSerializer.Serialize(new
                {
                    type = type,
                    id = exceptionId,
                    data = new { message = $"{message} ID={exceptionId}" }
                });

                _logger.LogError("type:{Type}, id:{ExceptionId}, data:[message:{Detail} ID={ExceptionId}]", type, exceptionId, message, exceptionId);
            }

            JournalDTO journalDTO = new JournalDTO
            {
                ExceptionId = exceptionId,
                StatusCode = httpContext.Response.StatusCode,
                Timestamp = timeStamp,
                TypeRequest = httpContext.Request.Path,
                QueryParameters = httpContext.Request.QueryString.Value,
                BodyParameters = bodyParameters,
                StackTrace = exception.StackTrace
            };

            using IServiceScope scope = _serviceProvider.CreateScope();
            IJournalRepository repository = scope.ServiceProvider.GetRequiredService<IJournalRepository>();
            await repository.Create(journalDTO, cancellationToken);

            await httpContext.Response.WriteAsync(responce, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}