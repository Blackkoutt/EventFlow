using EventFlowAPI.Logic.Response.Abstract;
using System.Net;

namespace EventFlowAPI.Logic.Response
{
    public class ConflictResponse : HttpResponse
    {
        public ConflictResponse(string message) : base(message)
        {
            Code = HttpStatusCode.Conflict;
            Title = "Conflict";
            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/409";
        }
    }
}
