﻿using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class OrganizerRespository(APIContext context) : GenericRepository<Organizer>(context), IOrganizerRepository
    {
    }
}
