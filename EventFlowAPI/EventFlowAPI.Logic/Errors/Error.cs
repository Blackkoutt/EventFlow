using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record Error(HttpResponse? Details = null)
    {
        public static readonly Error None = new();
        public static readonly Error NullParameter = new(new BadRequestResponse("Request body param is null."));
        public static readonly Error RouteParamOutOfRange = new(new BadRequestResponse("Route param is out of range."));
        public static readonly Error QueryParamOutOfRange = new(new BadRequestResponse("Query param is out of range."));
        public static readonly Error NotFound = new(new BadRequestResponse("Entity with given Id does not exist in database."));
        public static readonly Error SuchEntityExistInDb = new(new BadRequestResponse("Entity with given name already exist in database."));
        public static readonly Error EntityIsExpired = new(new BadRequestResponse("Can not perform action beacuse entity is expired."));
        public static readonly Error EntityIsCanceled = new(new BadRequestResponse("Can not perform action beacuse entity was canceled some time before."));
       
    }    
}
