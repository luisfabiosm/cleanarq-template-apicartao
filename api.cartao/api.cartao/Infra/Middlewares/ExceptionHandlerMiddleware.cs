using Domain.Core.Base;
using System.Diagnostics;
using System.Text.Json;

namespace Infra.Middlewares
{

    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null)
                    ex = ex.InnerException;

                await ConfigExceptionTypesAsync(httpContext, ex);
            }
        }

        private Task ConfigExceptionTypesAsync(HttpContext context, Exception exception)
        {
            using (var activity = Activity.Current?.Source.StartActivity($"Exception Middleware"))
            {
                BaseError errorObject = new("500", exception.Message, exception.StackTrace);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                activity?.SetTag("Exception:  ", $"Codigo: {errorObject.code} \n Mensagem:{errorObject.message}");
                activity?.SetTag("Location of Error", exception.StackTrace);
                return context.Response.WriteAsync(JsonSerializer.Serialize(errorObject));
            }
        }
    }
}
