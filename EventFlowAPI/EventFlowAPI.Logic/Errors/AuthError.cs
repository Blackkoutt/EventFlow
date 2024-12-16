using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record AuthError(Dictionary<string, string> errors)
    {
        public static readonly Error EmailAlreadyTaken = new(new BadRequestResponse("This email is already taken. Please use another."));
        public static readonly Error UserHaventIdClaim = new(new BadRequestResponse("User haven't ID claim."));
        public static readonly Error UserNotVerified = new(new BadRequestResponse("User is not verified."));
        public readonly Error ErrorsWhileCreatingUser = new(new BadRequestResponse(errors));
        public static readonly Error InvalidEmailOrPassword = new(new UnauthorizedResponse("Nieprawidłowy login lub hasło."));
        public static readonly Error CanNotConfirmIdentity = new(new UnauthorizedResponse("Identity can't be confirmed."));
        public static readonly Error CanNotFoundUserInDB = new(new UnauthorizedResponse("Can't found user in database."));
        public static readonly Error GoogleTokenVerificationFailed = new(new UnauthorizedResponse("Google token verification failed."));
        public static readonly Error CannotExchangeCodeForToken = new(new UnauthorizedResponse("Can't exchange code for Google Bearer token."));
        public static readonly Error FailedToGetUserData = new(new UnauthorizedResponse("Failed to get user data."));
        public static readonly Error FailedToGetUserEmail = new(new UnauthorizedResponse("Failed to get user email."));
        
        public static readonly Error UserDoesNotHaveSpecificRole = new(new ForbiddenResponse("User does not have specific role to access this resource."));
        public static readonly Error UserDoesNotHavePremissionToResource = new(new ForbiddenResponse("User does not have premission to access this resource."));
       
        public static readonly Error HttpContextNotAvailable = new(new BadRequestResponse("Can't access http context."));
    }
}
