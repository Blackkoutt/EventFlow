using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record MailSenderError(HttpResponse? Details = null)
    {
        public static readonly Error UserEmailIsNullOrEmpty = new(new BadRequestResponse("Cannot send information via email because user email is null or empty."));
    }
}
