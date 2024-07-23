namespace EventFlowAPI.DB.Models
{
    public class Festival_Organizer
    {
        public int FestivalId { get; set; }
        public int OrganizerId { get; set; }

        public Festival? Festival { get; set; }
        public Organizer? Organizer { get; set; }
    }
}
