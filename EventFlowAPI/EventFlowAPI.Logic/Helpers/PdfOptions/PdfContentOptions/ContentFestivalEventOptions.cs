using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfContentOptions
{
    public class ContentFestivalEventOptions
    {
        private readonly Reservation _reservation;
        public ContentFestivalEventOptions(Reservation reservation)
        {
            _reservation = reservation;
            Festival = new ContentFestivalOptions(_reservation);
            Event = new ContentEventOptions(_reservation);
        }

        //Common       
        public bool IsFestival => _reservation.Ticket.Festival != null;
        public float PadLeft => 10f;

        //Festival
        public ContentFestivalOptions Festival { get; private set; }

        // Event
        public ContentEventOptions Event { get; private set; }
    }
}
