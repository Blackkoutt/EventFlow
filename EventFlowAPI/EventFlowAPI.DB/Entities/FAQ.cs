using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.DB.Entities
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;  
    }
}
