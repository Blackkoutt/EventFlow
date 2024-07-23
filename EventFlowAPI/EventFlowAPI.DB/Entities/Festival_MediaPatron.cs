namespace EventFlowAPI.DB.Entities
{
    public class Festival_MediaPatron
    {
        public int FestivalId { get; set; }
        public int MediaPatronId { get; set; }

        public Festival? Festival { get; set; }
        public MediaPatron? MediaPatron { get; set; }
    }
}
