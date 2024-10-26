using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class TicketService(IUnitOfWork unitOfWork) :
        /*GenericService<
            Ticket,
            TicketRequestDto,
            TicketResponseDto
        >(unitOfWork),*/
        ITicketService
    {
        private readonly IGenericRepository<Ticket> _repository = unitOfWork.GetRepository<Ticket>();
        public async Task UpdateTicketsForFestival(ICollection<Event_FestivalTicketRequestDto> newFestivalTickets, Festival festival)
        {
            ICollection<Ticket> updatedTickets = [];

            var oldTicketTypesIds = festival.Tickets.Select(t => t.TicketTypeId).ToList();

            foreach(var newTicket in newFestivalTickets)
            {
                if (oldTicketTypesIds.Contains(newTicket.TicketTypeId))
                {
                    var oldFestivalTickets = festival.Tickets.Where(t => t.TicketTypeId == newTicket.TicketTypeId);
                    foreach(var festivalTicket in oldFestivalTickets)
                    {
                        if (festivalTicket.Price != newTicket.Price)
                        {
                            festivalTicket.Price = newTicket.Price;
                            _repository.Update(festivalTicket);
                        }
                        updatedTickets.Add(festivalTicket);
                    }
                }
                else
                {
                    foreach(var eventEntity in festival.Events)
                    {       
                        var festivalTicket = new Ticket
                        {
                            Price = newTicket.Price,
                            TicketTypeId = newTicket.TicketTypeId,
                            EventId = eventEntity.Id,
                            FestivalId = festival.Id,
                        };
                        await _repository.AddAsync(festivalTicket);
                    }
                }
            }
            if (updatedTickets.Count() != festival.Tickets.Count())
            {
                var ticketsToDelete = festival.Tickets.Except(updatedTickets);
                foreach (var ticket in ticketsToDelete)
                {
                    if (ticket.Reservations.Any())
                    {
                        ticket.IsDeleted = true;
                        _repository.Update(ticket);
                    }
                    else
                    {
                        _repository.Delete(ticket);
                    }
                }
            }
        }

        public async Task UpdateTicketsForEvent(ICollection<Event_FestivalTicketRequestDto> newEventTickets, Event oldEvent)
        {
            ICollection<Ticket> updatedTickets = [];

            var oldTicketTypesIds = oldEvent.Tickets.Where(t => t.Festival == null).Select(t => t.TicketTypeId).ToList();
            foreach (var newTicket in newEventTickets)
            {
                if (oldTicketTypesIds.Contains(newTicket.TicketTypeId))
                {
                    var oldTicket = oldEvent.Tickets.Where(t =>
                                        t.Festival == null &&
                                        t.TicketTypeId == newTicket.TicketTypeId).First();

                    if (oldTicket.Price != newTicket.Price)
                    {
                        oldTicket.Price = newTicket.Price;
                        _repository.Update(oldTicket);
                    }
                    updatedTickets.Add(oldTicket);
                }
                else
                {
                    var eventTicket = new Ticket
                    {
                        Price = newTicket.Price,
                        TicketTypeId = newTicket.TicketTypeId,
                        EventId = oldEvent.Id,
                    };
                    await _repository.AddAsync(eventTicket);
                }
            }
            if (updatedTickets.Count() != oldEvent.Tickets.Where(t => t.Festival == null).Count())
            {
                var ticketsToDelete = oldEvent.Tickets.Where(t => t.Festival == null).Except(updatedTickets);
                foreach (var ticket in ticketsToDelete)
                {
                    if (ticket.Reservations.Any())
                    {
                        ticket.IsDeleted = true;
                        _repository.Update(ticket);
                    }
                    else
                    {
                        _repository.Delete(ticket);
                    }
                }
            }
        }

        public ICollection<Ticket> GetFestivalTickets(ICollection<Event_FestivalTicketRequestDto> ticketsDto, ICollection<Event> festivalEventList)
        {
            ICollection<Ticket> tickets = [];
            foreach (var ticket in ticketsDto)
            {
                foreach(var eventEntity in festivalEventList)
                {
                    var eventTicket = new Ticket
                    {
                        Price = ticket.Price,
                        TicketTypeId = ticket.TicketTypeId,
                        EventId = eventEntity.Id,
                    };
                    tickets.Add(eventTicket);
                }
            }
            return tickets;
        }

        public ICollection<Ticket> GetEventTickets(ICollection<Event_FestivalTicketRequestDto> ticketsDto)
        {
            ICollection<Ticket> tickets = [];
            foreach (var ticket in ticketsDto)
            {
                var eventTicket = new Ticket
                {
                    Price = ticket.Price,
                    TicketTypeId = ticket.TicketTypeId,
                };
                tickets.Add(eventTicket);
            }
            return tickets;
        }

        public async Task DeleteTickets(ICollection<Event> eventsToDelete, ICollection<Festival> festivalsToDelete)
        {
            var eventIds = eventsToDelete.Select(e => e.Id);
            var festivalIds = festivalsToDelete.Select(f => (int?)f.Id);

            var tickets = await _repository
                                   .GetAllAsync(q => q.Where(t =>
                                   !t.IsDeleted &&
                                   eventIds.Contains(t.EventId) ||
                                   festivalIds.Contains(t.FestivalId)));

            foreach (var ticket in tickets)
            {
                if (ticket.Reservations.Any())
                {
                    ticket.IsDeleted = true;
                    _repository.Update(ticket);
                }
                else
                {
                    _repository.Delete(ticket);
                }
            }
        }


        /*protected sealed override IEnumerable<TicketResponseDto> MapAsDto(IEnumerable<Ticket> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<TicketResponseDto>();
                responseDto.TicketType = entity.TicketType.AsDto<TicketTypeResponseDto>();
                responseDto.Event = entity.Event.AsDto<EventResponseDto>();
                responseDto.Event.Category = entity.Event.Category.AsDto<EventCategoryResponseDto>();
                //responseDto.Festival = entity.Festival?.AsDto<FestivalResponseDto>();
                return responseDto;
            });
        }*/
    

        /*protected sealed override TicketResponseDto MapAsDto(Ticket entity)
        {
            entity.AsDto<TicketResponseDto>();
        }*/
                                       

       /* protected async sealed override Task<bool> IsSameEntityExistInDatabase(TicketRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.TicketTypeId == entityDto.TicketTypeId &&
                        entity.EventId == entityDto.EventId
                      )
                  );

            return IsEntityWithOtherIdExistInList(entities, id);
        }*/
    }
}
