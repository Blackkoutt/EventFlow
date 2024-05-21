using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class HallRent_AdditionalServices
    {
        public int HallRentId { get; set; }
        public int AdditionalServiceId { get; set; }
        public HallRent? HallRent { get; set; }
        public AdditionalServices? AdditionalService { get; set; }
    }
}
