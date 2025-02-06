using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record AuthError(Dictionary<string, string> errors)
    {
        public static readonly Error EmailAlreadyTaken = new(new BadRequestResponse("Adres e-mail jest już zajęty. Użyj innego."));
        public static readonly Error UserHaventIdClaim = new(new BadRequestResponse("Użytkownik nie ma ID w claims"));
        public static readonly Error UserNotVerified = new(new BadRequestResponse("Użytkownik nie jest zweryfikowany."));
        public readonly Error ErrorsWhileCreatingUser = new(new BadRequestResponse(errors));
        public static readonly Error InvalidEmailOrPassword = new(new UnauthorizedResponse("Nieprawidłowy login lub hasło."));
        public static readonly Error CanNotConfirmIdentity = new(new UnauthorizedResponse("Nie można potwierdzić tożsamości."));
        public static readonly Error CanNotFoundUserInDB = new(new UnauthorizedResponse("Nie można znaleźć użytkownika w bazie danych."));
        public static readonly Error GoogleTokenVerificationFailed = new(new UnauthorizedResponse("Weryfikacja tokenu Google nie powiodła się."));
        public static readonly Error CannotExchangeCodeForToken = new(new UnauthorizedResponse("Nie można wymienić kodu na token Google Bearer."));
        public static readonly Error FailedToGetUserData = new(new UnauthorizedResponse("Nie udało się pobrać danych użytkownika."));
        public static readonly Error FailedToGetUserEmail = new(new UnauthorizedResponse("Nie udało się pobrać adresu e-mail użytkownika."));

        public static readonly Error UserDoesNotHaveSpecificRole = new(new ForbiddenResponse("Użytkownik nie ma odpowiedniej roli do dostępu do tego zasobu."));
        public static readonly Error UserDoesNotHavePremissionToResource = new(new ForbiddenResponse("Użytkownik nie ma uprawnień do dostępu do tego zasobu."));

        public static readonly Error HttpContextNotAvailable = new(new BadRequestResponse("Nie można uzyskać dostępu do kontekstu HTTP."));
    }
}
