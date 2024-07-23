using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class FestivalDetailsRepository(APIContext context) : GenericRepository<FestivalDetails>(context), IFestivalDetailsRepository
    {
    }
}
