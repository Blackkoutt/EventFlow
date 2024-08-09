using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Exceptions;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.Repositories.Repositories;

namespace EventFlowAPI.Logic.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly APIContext _context;
        private readonly Dictionary<Type, IRepository> _repositories;
        public UnitOfWork(APIContext context) 
        {
            _context = context;

            _additionalServices = new AdditionalServicesRepository(_context);
            _equipments = new EquipmentRepository(_context);
            _eventCategories = new EventCategoryRepository(_context);
            _eventDetails = new EventDetailsRepository(_context);
            _eventPasses = new EventPassRepository(_context);
            _eventPassTypes = new EventPassTypeRepository(_context);
            _events = new EventRepository(_context);
            _eventTickets = new EventTicketRepository(_context);
            _festival_Events = new Festival_EventRepository(_context);
            _festival_MediaPatrons = new Festival_MediaPatronRepository(_context);
            _festival_Organizers = new Festival_OrganizerRepository(_context);
            _festival_Sponsors = new Festival_SponsorRepository(_context);
            _festivalDetails = new FestivalDetailsRepository(_context);
            _festivals = new FestivalRepository(_context);
            _hallRent_AdditionalServices = new HallRent_AdditionalServicesRepository(_context);
            _hallRents = new HallRentRespository(_context);
            _halls = new HallRepository(_context);
            _hallType_Equipments = new HallType_EquipmentRepository(_context);
            _hallTypes = new HallTypeRepository(_context);
            _mediaPatrons = new MediaPatronRepository(_context);
            _organizers = new OrganizerRespository(_context);
            _paymentTypes = new PaymentTypeRepository(_context);
            _reservation_Seats = new Reservation_SeatRepository(_context);
            _reservations = new ReservationRepository(_context);
            _seats = new SeatRepository(_context);
            _seatTypes = new SeatTypeRepository(_context);
            _sponsors = new SponsorRepository(_context);
            _ticketTypes = new TicketTypeRepository(_context);
            _usersData = new UserDataRepository(_context);
            _users = new UserRepository(_context);

            _repositories = InitRepositoriesDictionary();
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>()  where TEntity : class 
        {
            if (_repositories.TryGetValue(typeof(TEntity), out var repository))
            {
                return (IGenericRepository<TEntity>)repository;
            }
            throw new RepositoryNotExistException($"No existing repository for entity type {typeof(TEntity)}.");
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            //DbUpdateException
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        private Dictionary<Type, IRepository> InitRepositoriesDictionary()
        {
            return new Dictionary<Type, IRepository>
            {
                { typeof(AdditionalServices), _additionalServices },
                { typeof(Equipment), _equipments },
                { typeof(EventCategory), _eventCategories },
                { typeof(EventDetails), _eventDetails },
                { typeof(EventPass), _eventPasses },
                { typeof(EventPassType), _eventPassTypes },
                { typeof(Event), _events },
                { typeof(EventTicket), _eventTickets },
                { typeof(Festival_Event), _festival_Events },
                { typeof(Festival_MediaPatron), _festival_MediaPatrons },
                { typeof(Festival_Organizer), _festival_Organizers },
                { typeof(Festival_Sponsor), _festival_Sponsors },
                { typeof(FestivalDetails), _festivalDetails },
                { typeof(Festival), _festivals },
                { typeof(HallRent_AdditionalServices), _hallRent_AdditionalServices },
                { typeof(HallRent), _hallRents },
                { typeof(Hall), _halls },
                { typeof(HallType_Equipment), _hallType_Equipments },
                { typeof(HallType),  _hallTypes },
                { typeof(MediaPatron), _mediaPatrons },
                { typeof(Organizer), _organizers },
                { typeof(PaymentType), _paymentTypes },
                { typeof(Reservation_Seat), _reservation_Seats },
                { typeof(Reservation), _reservations },
                { typeof(Seat), _seats },
                { typeof(SeatType), _seatTypes },
                { typeof(Sponsor), _sponsors },
                { typeof(TicketType), _ticketTypes },
                { typeof(UserData), _usersData },
                { typeof(User), _users},
            };
        }

        private readonly IAdditionalServicesRepository _additionalServices;
        private readonly IEquipmentRepository _equipments;
        private readonly IEventCategoryRepository _eventCategories;
        private readonly IEventDetailsRepository _eventDetails;
        private readonly IEventPassRepository _eventPasses;
        private readonly IEventPassTypeRepository _eventPassTypes;
        private readonly IEventRepository _events;
        private readonly IEventTicketRepository _eventTickets;
        private readonly IFestival_EventRepository _festival_Events;
        private readonly IFestival_MediaPatronRepository _festival_MediaPatrons;
        private readonly IFestival_OrganizerRepository _festival_Organizers;
        private readonly IFestival_SponsorRepository _festival_Sponsors;
        private readonly IFestivalDetailsRepository _festivalDetails;
        private readonly IFestivalRepository _festivals;
        private readonly IHallRent_AdditionalServicesRepository _hallRent_AdditionalServices;
        private readonly IHallRentRepository _hallRents;
        private readonly IHallRepository _halls;
        private readonly IHallType_EquipmentRepository _hallType_Equipments;
        private readonly IHallTypeRepository _hallTypes;
        private readonly IMediaPatronRepository _mediaPatrons;
        private readonly IOrganizerRepository _organizers;
        private readonly IPaymentTypeRepository _paymentTypes;
        private readonly IReservation_SeatRepository _reservation_Seats;
        private readonly IReservationRepository _reservations;
        private readonly ISeatRepository _seats;
        private readonly ISeatTypeRepository _seatTypes;
        private readonly ISponsorRepository _sponsors;
        private readonly ITicketTypeRepository _ticketTypes;
        private readonly IUserDataRepository _usersData;
        private readonly IUserRepository _users;
    }
}
