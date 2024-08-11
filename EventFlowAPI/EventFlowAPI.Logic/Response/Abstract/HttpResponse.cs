using System.Net;

namespace EventFlowAPI.Logic.Response.Abstract
{
    public abstract class HttpResponse
    {
        protected HttpResponse(string message)
        {
            Details = new Dictionary<string, object>
            {
                { "errors", new[] { message } }
            };
        }
        public HttpStatusCode? Code { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, object> Details { get; set; }
    }
}
