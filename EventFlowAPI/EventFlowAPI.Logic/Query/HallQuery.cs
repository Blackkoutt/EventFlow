using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class HallQuery : QueryObject
    {
        public int? HallNr { get; set; }
        public DateTime? RentFromDate { get; set; }
        public DateTime? RentToDate { get; set; }
        public decimal? MinRentalPrice { get; set; }
        public decimal? MaxRentalPrice { get; set; }
        public int? Floor { get; set; }
        public int? HallTypeId { get; set; }
        public int? MinSeatsCount { get; set; }
        public int? MaxSeatsCount { get; set; }
        public decimal? MinHallArea { get; set; }
        public decimal? MaxHallArea { get; set; }
        public bool? IsHallHaveStage { get; set; }
    }
}
