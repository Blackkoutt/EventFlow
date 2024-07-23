using EventFlowAPI.Logic.Repositories.Interfaces;

namespace EventFlowAPI.Logic.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAdditionalServicesRepository AdditionalServices { get; }
        IEquipmentRepository Equipments { get; }
        IEventCategoryRepository EventCategories { get; }
        IEventDetailsRepository EventDetails { get; }
        IEventPassRepository EventPasses { get; }
        IEventPassTypeRepository EventPassTypes { get; }
        IEventRepository Events { get; }
        IEventTicketRepository EventTickets { get; }
        IFestival_EventRepository Festival_Events { get; }
        IFestival_MediaPatronRepository Festival_MediaPatrons { get; }
        IFestival_OrganizerRepository Festival_Organizers { get; }
        IFestival_SponsorRepository Festival_Sponsors { get; }
        IFestivalDetailsRepository FestivalDetails { get; }
        IFestivalRepository Festivals { get; }
        IHallRent_AdditionalServicesRepository HallRent_AdditionalServices { get; }
        IHallRentRepository HallRents { get; }
        IHallRepository Halls { get; }
        IHallType_EquipmentRepository HallType_Equipments { get; }
        IHallTypeRepository HallTypes { get; }
        IMediaPatronRepository MediaPatrons { get; }
        IOrganizerRepository Organizers { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        IReservation_SeatRepository Reservation_Seats { get; }
        IReservationRepository Reservations { get; }
        ISeatRepository Seats { get; }
        ISeatTypeRepository SeatTypes { get; }
        ISponsorRepository Sponsors { get; }
        ITicketTypeRepository TicketTypes { get; }
        IUserDataRepository UsersData { get; }
        IUserRepository Users { get; }
        Task SaveChangesAsync();
    }
}
