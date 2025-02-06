using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record SeatTypeError(HttpResponse? Details = null)
    {
        public static readonly Error SeatTypeNotFound = new(new BadRequestResponse("Typ miejsca o podanym ID nie istnieje w bazie danych."));
        public static readonly Error CannotDeleteDefaultSeat = new(new BadRequestResponse("Nie można usunąć domyślnego miejsca."));
        public static readonly Error CannotFoundDefaultSeat = new(new BadRequestResponse("Nie można znaleźć domyślnego miejsca."));
    }
}
