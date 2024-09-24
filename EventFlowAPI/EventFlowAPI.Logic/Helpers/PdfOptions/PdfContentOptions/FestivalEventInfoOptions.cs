using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfContentOptions
{
    public class FestivalEventInfoOptions
    {
        private readonly Reservation _reservation;
        public FestivalEventInfoOptions(Reservation reservation)
        {
            _reservation = reservation;
            Festival = new FestivalInfoOptions(_reservation);
            Event = new EventInfoOptions(_reservation);
        }

        //Common       
        public bool IsFestival => _reservation.Ticket.Festival != null;
        public float PadLeft => 10f;

        //Festival
        public FestivalInfoOptions Festival { get; private set; }

        // Event
        public EventInfoOptions Event { get; private set; }
    }
}
