using AutoMapper;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;

namespace EventFlowAPI.Logic.Mapper.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AdditionalServicesRequestDto, AdditionalServices>();
            CreateMap<AdditionalServices, AdditionalServicesResponseDto>();

            CreateMap<EquipmentRequestDto, Equipment>();
            CreateMap<Equipment, EquipmentResponseDto>();

            CreateMap<EventCategoryRequestDto, EventCategory>();
            CreateMap<EventCategory, EventCategoryResponseDto>();

            CreateMap<EventDetailsRequestDto, EventDetails>();
            CreateMap<EventDetails, EventDetailsResponseDto>();

            CreateMap<EventPassRequestDto, EventPass>();
            CreateMap<EventPass, EventPassResponseDto>();

            CreateMap<EventPassTypeRequestDto, EventPassType>();
            CreateMap<EventPassType, EventPassTypeResponseDto>();

            CreateMap<EventRequestDto, Event>();
            CreateMap<Event, EventResponseDto>();

            CreateMap<EventTicketRequestDto, EventTicket>();
            CreateMap<EventTicket, EventTicketResponseDto>();

            CreateMap<FestivalDetailsRequestDto, FestivalDetails>();
            CreateMap<FestivalDetails, FestivalDetailsResponseDto>();

            CreateMap<FestivalRequestDto, Festival>();
            CreateMap<Festival, FestivalDetailsResponseDto>();

            CreateMap<HallRentRequestDto, HallRent>();
            CreateMap<HallRent, HallRentResponseDto>();

            CreateMap<HallRequestDto, Hall>();
            CreateMap<Hall, HallResponseDto>();

            CreateMap<HallTypeRequestDto, HallType>();
            CreateMap<HallType, HallTypeResponseDto>();

            CreateMap<MediaPatronRequestDto, MediaPatron>();
            CreateMap<MediaPatron, MediaPatronResponseDto>();

            CreateMap<OrganizerRequestDto, Organizer>();
            CreateMap<Organizer, OrganizerResponseDto>();

            CreateMap<PaymentTypeRequestDto, PaymentType>();
            CreateMap<PaymentType, PaymentTypeResponseDto>();

            CreateMap<ReservationRequestDto, Reservation>();
            CreateMap<Reservation, ReservationResponseDto>();

            CreateMap<SeatRequestDto, Seat>();
            CreateMap<Seat, SeatRequestDto>();

            CreateMap<SeatTypeRequestDto, SeatType>();
            CreateMap<SeatType, SeatTypeResponseDto>();

            CreateMap<SponsorRequestDto, Sponsor>();
            CreateMap<Sponsor, SponsorResponseDto>();

            CreateMap<TicketTypeRequestDto, TicketType>();
            CreateMap<TicketType, TicketTypeResponseDto>();

            CreateMap<UserDataRequestDto, UserData>();
            CreateMap<UserData, UserDataResponseDto>();

            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
