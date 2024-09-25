using EventFlowAPI.Logic.Response.Abstract;
using System.Net;

namespace EventFlowAPI.Logic.Response
{
    public class ForbiddenResponse : HttpResponse
    {
        public ForbiddenResponse(string message) : base(message)
        {
            Code = HttpStatusCode.Forbidden;
            Title = "Forbidden";
            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/403";
        }
    }
}
