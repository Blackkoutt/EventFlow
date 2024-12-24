using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record PaymentError(HttpResponse? Details = null)
    {
        public static readonly Error ClientIdOrSecretError = new(new UnauthorizedResponse("Błąd autoryzacji w serwisie PayU: Can not found ClientId or Secret."));
        public static readonly Error BadClientCredentials = new(new UnauthorizedResponse("Błąd autoryzacji w serwisie PayU: Bad Client Credentials."));
        public static readonly Error CreateTransactionBadRequest = new(new BadRequestResponse("Błąd podczas tworzenia transakcji w serwisie PayU."));
        public static readonly Error GetTransactionStatusBadRequest = new(new BadRequestResponse("Błąd podczas pobierania statusu transakcji w serwisie PayU."));
        public static readonly Error SerializeError = new(new BadRequestResponse("Błąd podczas serializacji odpowiedzi z serwisu PayU."));
        public static readonly Error OrderStatusIsEmptyOrNull = new(new BadRequestResponse("Nie udało się odnaleźć statusu transakcji."));
        public static readonly Error BadTransactionId = new(new BadRequestResponse("Nie podano Id transakcji."));
        public static readonly Error PaymentDescriptionEmpty = new(new BadRequestResponse("Nie podano opisu transakcji."));
        public static readonly Error PaymentContinueUrlEmpty = new(new BadRequestResponse("Nie podano adresu przekierowania po udanej transakcji."));
        public static readonly Error TotalAmountIsZero = new(new BadRequestResponse("Kwota do zapłaty nie może wynosić 0 PLN."));
        public static readonly Error ProductArrayEmpty = new(new BadRequestResponse("Należy podać conajmniej jeden produkt do zakupu."));
        public static readonly Error ProductBadRequest = new(new BadRequestResponse("Należy podać poprawne dane produktu."));
        public static readonly Error BuyerBadRequest = new(new BadRequestResponse("Należy podać poprawne dane kupującego."));
        public static readonly Error TransactionIsNotCompleted = new(new BadRequestResponse("Transakcja nie została zakończona pomyślnie."));
        public static readonly Error TransactionIsPendingTooLong = new(new BadRequestResponse("Transakcja zbyt długo oczekuje na zatwierdzenie."));
    }
}
