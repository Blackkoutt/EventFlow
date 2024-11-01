using EventFlowAPI.Logic.Response.Abstract;
using System.Net;

namespace EventFlowAPI.Logic.Response
{
    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse(string message) : base(message)
        {
            Code = HttpStatusCode.NotFound;
            Title = "Not Found";
            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404";
        }
    }
}
