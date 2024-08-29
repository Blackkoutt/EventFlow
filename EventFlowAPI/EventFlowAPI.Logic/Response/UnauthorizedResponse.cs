using EventFlowAPI.Logic.Response.Abstract;
using System.Net;

namespace EventFlowAPI.Logic.Response
{
    public class UnauthorizedResponse : HttpResponse
    {
        public UnauthorizedResponse(string message) : base(message)
        {
            Code = HttpStatusCode.Unauthorized;
            Title = "Unauthorized";
            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401";
        }
    }
}
