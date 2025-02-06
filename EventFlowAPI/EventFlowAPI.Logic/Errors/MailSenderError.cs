using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record MailSenderError(HttpResponse? Details = null)
    {
        public static readonly Error UserEmailIsNullOrEmpty = new(new BadRequestResponse("Nie można wysłać maila, ponieważ adres e-mail użytkownika jest null."));
    }
}
