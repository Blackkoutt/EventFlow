using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class EventDetailsRepository : Repository<EventDetails>, IEventDetailsRepository
    {
        public EventDetailsRepository(APIContext context) : base(context) { }
    }
}
