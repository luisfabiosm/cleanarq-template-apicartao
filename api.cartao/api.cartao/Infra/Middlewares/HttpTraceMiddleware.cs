namespace Infra.Middlewares
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Domain.Core.Base;
    using Domain.Core.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using OpenTelemetry.Trace;



    public class HttpTraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private Stream? originalResponseBody;

        public HttpTraceMiddleware(RequestDelegate next, ILogger<HttpTraceMiddleware> logger, ActivitySource activitySource)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);



            _logger.LogInformation(request);

            var originalBodyStream = context.Response.Body;

            using MemoryStream responseBody = new MemoryStream();
            try
            {

                context.Response.Body = responseBody;
                originalResponseBody = context.Response.Body;
                await _next(context);

                var response = await FormatResponse(context.Response);
                var statusCode = context.Response.StatusCode;

                if (statusCode >= 400)
                {
                    _logger.LogError(response);
                }
                else
                {
                    _logger.LogInformation(response);
                }

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null)
                    ex = ex.InnerException;

                await ConfigExceptionTypesAsync(context, ex, responseBody, originalResponseBody);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;

                return $"{request.Method} {request.Path} {request.QueryString} {body}";
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var body = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"HTTP {response.StatusCode} {body}";
        }

        private Task ConfigExceptionTypesAsync(HttpContext context, Exception exception, MemoryStream responseBody, Stream? originalResponseBody)
        {
            responseBody.Position = 0;
            responseBody.CopyToAsync(originalResponseBody);
            context.Response.Body = originalResponseBody;

            _logger.LogError($"Thread[{Thread.GetCurrentProcessorId().ToString("000000")}]  ERRO:{context.Request.Method} - {exception.Message}");
            _logger.LogError($"Thread[{Thread.GetCurrentProcessorId().ToString("000000")}]  [StackError] {exception.StackTrace}");

            var errorRet = exception switch
            {
                RequestHeaderException => new BaseReturn().SystemException(exception),
                _ => new BaseReturn().SystemException(exception),
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorRet.StatusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorRet));

        }
    }

}
