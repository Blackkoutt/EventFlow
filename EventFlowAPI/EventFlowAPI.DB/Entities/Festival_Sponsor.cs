namespace EventFlowAPI.DB.Entities
{
    public class Festival_Sponsor
    {
        public int FestivalId { get; set; }
        public int SponsorId { get; set; }

        public Festival? Festival { get; set; }
        public Sponsor? Sponsor { get; set; }
    }
}
