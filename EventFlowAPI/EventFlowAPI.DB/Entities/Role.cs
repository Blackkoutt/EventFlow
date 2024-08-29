using EventFlowAPI.DB.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace EventFlowAPI.DB.Entities
{
    public class Role : IdentityRole, IEntity
    {
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; } = [];
    }
}
