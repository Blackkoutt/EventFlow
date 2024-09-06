using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record PaymentTypeError(HttpResponse? Details = null)
    {
        public static readonly Error PaymentTypeNotFound = new(new BadRequestResponse("Payment type with given Id does not exist in database."));
    }
}
