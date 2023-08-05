using System.Net;

namespace WebShop.WebShop.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
    public class CustomExceptionWithResponse : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object Response { get; }

        public CustomExceptionWithResponse(object response, RequestContext context)
            : base(context.Message)
        {
            StatusCode = context.StatusCode;
            Response = response;
        }
    }
    public class RequestContext
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public RequestContext(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
