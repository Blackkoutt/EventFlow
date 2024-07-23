using EventFlowAPI.DB.Context;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories;

namespace EventFlowAPI.Logic.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly APIContext _context;
        public UnitOfWork(APIContext context) 
        {
            _context = context;

            AdditionalServices = new AdditionalServicesRepository(_context);
            Equipments = new EquipmentRepository(_context);
            EventCategories = new EventCategoryRepository(_context);
            EventDetails = new EventDetailsRepository(_context);
            EventPasses = new EventPassRepository(_context);
            EventPassTypes = new EventPassTypeRepository(_context);
            Events = new EventRepository(_context);
            EventTickets = new EventTicketRepository(_context);
            Festival_Events = new Festival_EventRepository(_context);
            Festival_MediaPatrons = new Festival_MediaPatronRepository(_context);
            Festival_Organizers = new Festival_OrganizerRepository(_context);
            Festival_Sponsors = new Festival_SponsorRepository(_context);
            FestivalDetails = new FestivalDetailsRepository(_context);
            Festivals = new FestivalRepository(_context);
            HallRent_AdditionalServices = new HallRent_AdditionalServicesRepository(_context);
            HallRents = new HallRentRepository(_context);
            Halls = new HallRepository(_context);
            HallType_Equipments = new HallType_EquipmentRepository(_context);
            HallTypes = new HallTypeRepository(_context);
            MediaPatrons = new MediaPatronRepository(_context);
            Organizers = new OrganizerRepository(_context);
            PaymentTypes = new PaymentTypeRepository(_context);
            Reservation_Seats = new Reservation_SeatRepository(_context);
            Reservations = new ReservationRepository(_context);
            Seats = new SeatRepository(_context);
            SeatTypes = new SeatTypeRepository(_context);
            Sponsors = new SponsorRepository(_context);
            TicketTypes = new TicketTypeRepository(_context);
            UsersData = new UserDataRepository(_context);
            Users = new UserRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();   
        }

        public IAdditionalServicesRepository AdditionalServices { get; private set; }

        public IEquipmentRepository Equipments { get; private set; }

        public IEventCategoryRepository EventCategories { get; private set; }

        public IEventDetailsRepository EventDetails { get; private set; }

        public IEventPassRepository EventPasses { get; private set; }

        public IEventPassTypeRepository EventPassTypes { get; private set; }

        public IEventRepository Events { get; private set; }

        public IEventTicketRepository EventTickets { get; private set; }

        public IFestival_EventRepository Festival_Events { get; private set; }
        public IFestival_MediaPatronRepository Festival_MediaPatrons { get; private set; }

        public IFestival_OrganizerRepository Festival_Organizers { get; private set; }

        public IFestival_SponsorRepository Festival_Sponsors { get; private set; }

        public IFestivalDetailsRepository FestivalDetails { get; private set; }

        public IFestivalRepository Festivals { get; private set; }
        public IHallRent_AdditionalServicesRepository HallRent_AdditionalServices { get; private set; }

        public IHallRentRepository HallRents { get; private set; }

        public IHallRepository Halls { get; private set; }

        public IHallType_EquipmentRepository HallType_Equipments { get; private set; }

        public IHallTypeRepository HallTypes { get; private set; }

        public IMediaPatronRepository MediaPatrons { get; private set; }

        public IOrganizerRepository Organizers { get; private set; }

        public IPaymentTypeRepository PaymentTypes { get; private set; }

        public IReservation_SeatRepository Reservation_Seats { get; private set; }

        public IReservationRepository Reservations { get; private set; }

        public ISeatRepository Seats { get; private set; }

        public ISeatTypeRepository SeatTypes { get; private set; }

        public ISponsorRepository Sponsors { get; private set; }

        public ITicketTypeRepository TicketTypes { get; private set; }

        public IUserDataRepository UsersData { get; private set; }

        public IUserRepository Users { get; private set; }

    }
}
