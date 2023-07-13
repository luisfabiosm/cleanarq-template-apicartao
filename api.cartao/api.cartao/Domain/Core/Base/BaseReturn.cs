using System.Net;

namespace Domain.Core.Base
{
    public record BaseReturn
    {
        public object? Body;
        public BaseError? Error;
        public HttpStatusCode StatusCode = HttpStatusCode.OK;

        public BaseReturn()
        {

        }

        public void HttpSuccess<TBody>(TBody successObject)
        {
            Body = successObject;
        }


        public BaseReturn Sucesso(object successObject)
        {
            Body = successObject;
            StatusCode = HttpStatusCode.OK;
            return this;
        }


        public BaseReturn BussinesException(string message, string stack=null)
        {
            StatusCode = HttpStatusCode.BadRequest;

            Error = new()
            {
                code = "400",
                message = $"{message}",
                info = stack,
            };
            return this;
        }

        public BaseReturn SystemException(Exception ex)
        {
            StatusCode = HttpStatusCode.InternalServerError;

            Error = new()
            {
                code = "500",
                message = $"System Error: {ex.Message}",
                info = ex.StackTrace,
            };

            return this;
        }

        public object? GetBody()
        {
            if (StatusCode is HttpStatusCode.OK) return Body;
            return Error;
        }


        public IResult GetResponse()
        {
            if (StatusCode is HttpStatusCode.OK) return Results.Ok(Body);
            return Results.Json(Error, statusCode: (int)StatusCode);
        }


    }
}
