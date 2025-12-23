using BusinessLogicLayer.Exceptions;
using System.Net;
using System.Text.Json;

namespace FunDooApplication.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserNotFoundException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (PasswordInvalidException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.Unauthorized);
            }
            catch (EmailNotFoundException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.Conflict);
            }
            catch (NotesNotFoundException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (NotValidUserToCreateNote ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        private async Task HandleException(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
