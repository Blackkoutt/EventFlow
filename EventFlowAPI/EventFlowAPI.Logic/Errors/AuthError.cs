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
        public static readonly Error GoogleTokenVerificationFailed = new(new UnauthorizedResponse("Google token verification failed."));
        public static readonly Error CannotExchangeCodeForToken = new(new UnauthorizedResponse("Can't exchange code for Google Bearer token."));
        public static readonly Error FailedToGetUserData = new(new UnauthorizedResponse("Failed to get user data."));
        public static readonly Error FailedToGetUserEmail = new(new UnauthorizedResponse("Failed to get user email."));
        public static readonly Error HttpContextNotAvailable = new(new BadRequestResponse("Can't access http context."));
    }
}
