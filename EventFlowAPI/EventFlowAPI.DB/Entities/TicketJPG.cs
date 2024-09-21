using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.DB.Entities
{
    public class TicketJPG : BaseEntity, IFileEntity
    {
        public string FileName { get; set; } = string.Empty;
        public Guid ReservationGuid { get; set; }   
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
