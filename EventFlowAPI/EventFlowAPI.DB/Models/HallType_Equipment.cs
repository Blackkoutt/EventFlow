namespace EventFlowAPI.DB.Models
{
    public class HallType_Equipment
    {
        public int HallTypeId { get; set; } 
        public int EquipmentId { get; set; }
        public HallType? HallType { get; set; }
        public Equipment? Equipment { get; set; }
    }
}
