using System.Net;

namespace FAG_Board_Service.Exceptions;

public class HttpStatusException : Exception
{
    public HttpStatusException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; init; }
}