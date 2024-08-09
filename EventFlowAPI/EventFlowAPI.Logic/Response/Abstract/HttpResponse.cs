using System.Net;

namespace EventFlowAPI.Logic.Response.Abstract
{
    public abstract class HttpResponse(string message)
    {
        public string Message { get; set; } = message;
        public HttpStatusCode? Code { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
    }
}
