using Domain.Core.Base;
using System.Net;

namespace Domain.Core.Exceptions
{
    public class RequestHeaderException : Exception
    {
        public BaseError Error { get; private set; }
        public HttpStatusCode HttpReturnCode { get; private set; }

        public RequestHeaderException(string? message) : base(message)
        {
            HttpReturnCode = HttpStatusCode.BadRequest;
            Error = new BaseError
            {
                message = message,
                code = "400",
                info = "Header "
            };

        }


    }
}