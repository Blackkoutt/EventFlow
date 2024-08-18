﻿using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record HallTypeError(HttpResponse? Details = null)
    {
        public static readonly Error NullEquipmentsParameter = new(new BadRequestResponse("Equipments param in hall type request body is null."));
        public static readonly Error EquipmentNotFound = new(new BadRequestResponse("Equipment with given Id does not exist in database."));
    }
}