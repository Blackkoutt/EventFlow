﻿using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.Response.Abstract;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record Error(HttpResponse? Details = null)
    {
        public static readonly Error None = new();
        public static readonly Error NullParameter = new(new BadRequestResponse("Request body param is null."));
        public static readonly Error OutOfRangeId = new(new BadRequestResponse("Route param is out of range."));
        public static readonly Error NotFound = new(new BadRequestResponse("Entity with given Id does not exist in database."));
    }    
}