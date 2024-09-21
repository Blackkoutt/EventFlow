using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.DB.Entities
{
    public class TicketPDF : BaseEntity, IFileEntity
    {
        public string FileName { get; set; } = string.Empty;
        public Guid ReservationGuid { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
