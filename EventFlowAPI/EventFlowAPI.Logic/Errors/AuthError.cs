using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record AuthError(Dictionary<string, string> errors)
    {
        public static readonly Error EmailAlreadyTaken = new(new BadRequestResponse("This email is already taken. Please use another."));
        public readonly Error ErrorsWhileCreatingUser = new(new BadRequestResponse(errors));
        public static readonly Error InvalidEmailOrPassword = new(new UnauthorizedResponse("Invalid email or password."));
        public static readonly Error CanNotConfirmIdentity = new(new UnauthorizedResponse("Identity can't be confirmed."));
        public static readonly Error CanNotFoundUserInDB = new(new UnauthorizedResponse("Can't found user in database."));
    }
}
