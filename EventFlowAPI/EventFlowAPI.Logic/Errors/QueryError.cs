using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record QueryError
    {
        public static readonly Error BadQueryObject = new(new BadRequestResponse("Zły zapytanie w żądaniu."));
    }
}
