using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class FestivalDetails : BaseEntity
    {

        [MaxLength(2000)]
        public string? LongDescription { get; set; }
        public Festival? Festival { get; set; }
    }
}
