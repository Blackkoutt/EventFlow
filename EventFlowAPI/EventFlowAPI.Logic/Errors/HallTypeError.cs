using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record HallTypeError(HttpResponse? Details = null)
    {
        public static readonly Error NullEquipmentsParameter = new(new BadRequestResponse("Parametr 'Equipments' w ciele żądania typu sali jest null."));
        public static readonly Error EquipmentNotFound = new(new BadRequestResponse("Sprzęt o podanym ID nie istnieje w bazie danych."));
        public static readonly Error CannotDeleteDefaultHallType = new(new BadRequestResponse("Nie można usunąć domyślnego typu sali."));
        public static readonly Error CannotFoundDefaultHallType = new(new BadRequestResponse("Nie można znaleźć domyślnego typu sali."));
    }
}
