namespace EventFlowAPI.DB.Models
{
    public class Festival_MediaPatron
    {
        public int FestivalId { get; set; }
        public int MeidaPatronId { get; set; }

        public Festival? Festival { get; set; }
        public MediaPatron? MediaPatron { get; set; }
    }
}
