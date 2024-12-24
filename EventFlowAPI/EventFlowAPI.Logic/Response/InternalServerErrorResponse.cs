using EventFlowAPI.Logic.Response.Abstract;
using System.Net;

namespace EventFlowAPI.Logic.Response
{
    public class InternalServerErrorResponse : HttpResponse
    {
        public InternalServerErrorResponse(string message) : base(message)
        {
            Code = HttpStatusCode.InternalServerError;
            Title = "Internal Server Error";
            Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500";
        }
    }
}
