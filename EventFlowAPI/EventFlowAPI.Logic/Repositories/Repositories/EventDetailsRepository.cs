using EventFlowAPI.DB.Context;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class EventDetailsRepository(APIContext context) : GenericRepository<EventDetails>(context), IEventDetailsRepository
    {
    }
}
