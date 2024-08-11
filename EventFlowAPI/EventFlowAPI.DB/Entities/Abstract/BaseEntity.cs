using System.Text.Json.Serialization;

namespace EventFlowAPI.DB.Entities.Abstract
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
