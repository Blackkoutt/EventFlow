using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Identity.Services.Interfaces
{
    public interface IJWTGeneratorService
    {
        string GenerateToken(User user, IList<string>? roles);
    }
}
