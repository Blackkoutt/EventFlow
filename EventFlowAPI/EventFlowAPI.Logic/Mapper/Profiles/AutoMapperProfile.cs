﻿using AutoMapper;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;

namespace EventFlowAPI.Logic.Mapper.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AdditionalServices, AdditionalServices>();
            CreateMap<AdditionalServicesRequestDto, AdditionalServices>();
            CreateMap<AdditionalServices, AdditionalServicesResponseDto>();

            CreateMap<Equipment, Equipment>();
            CreateMap<EquipmentRequestDto, Equipment>();
            CreateMap<Equipment, EquipmentResponseDto>();

            CreateMap<EventCategory, EventCategory>();
            CreateMap<EventCategoryRequestDto, EventCategory>();
            CreateMap<EventCategory, EventCategoryResponseDto>();

            //CreateMap<EventDetailsRequestDto, EventDetails>();
            CreateMap<EventDetails, EventDetails>();
            CreateMap<EventDetails, EventDetailsResponseDto>();

            CreateMap<EventPass, EventPass>();
            CreateMap<EventPassRequestDto, EventPass>();
            CreateMap<EventPass, EventPassResponseDto>();

            CreateMap<EventPassType, EventPassType>();
            CreateMap<EventPassTypeRequestDto, EventPassType>();
            CreateMap<EventPassType, EventPassTypeResponseDto>();

            CreateMap<Event, Event>();
            CreateMap<EventRequestDto, Event>();
            CreateMap<Event, EventResponseDto>();

            CreateMap<EventTicket, EventTicket>();
            CreateMap<EventTicketRequestDto, EventTicket>();
            CreateMap<EventTicket, EventTicketResponseDto>();

            CreateMap<FestivalDetails, FestivalDetails>();
            CreateMap<FestivalDetailsRequestDto, FestivalDetails>();
            CreateMap<FestivalDetails, FestivalDetailsResponseDto>();

            CreateMap<Festival, Festival>();
            CreateMap<FestivalRequestDto, Festival>();
            CreateMap<Festival, FestivalDetailsResponseDto>();

            CreateMap<HallRent, HallRent>();
            CreateMap<HallRentRequestDto, HallRent>();
            CreateMap<HallRent, HallRentResponseDto>();

            CreateMap<Hall, Hall>();
            CreateMap<HallRequestDto, Hall>();
            CreateMap<Hall, HallResponseDto>();

            CreateMap<HallType, HallType>();
            CreateMap<HallTypeRequestDto, HallType>();
            CreateMap<HallType, HallTypeResponseDto>()
                .ForMember(dest => dest.Equipments, opt => opt.MapFrom(src => src.Equipments)); ;

            CreateMap<MediaPatron, MediaPatron>();
            CreateMap<MediaPatronRequestDto, MediaPatron>();
            CreateMap<MediaPatron, MediaPatronResponseDto>();

            CreateMap<Organizer, Organizer>();
            CreateMap<OrganizerRequestDto, Organizer>();
            CreateMap<Organizer, OrganizerResponseDto>();

            CreateMap<PaymentType, PaymentType>();
            CreateMap<PaymentTypeRequestDto, PaymentType>();
            CreateMap<PaymentType, PaymentTypeResponseDto>();

            CreateMap<Reservation, Reservation>();
            CreateMap<ReservationRequestDto, Reservation>();
            CreateMap<Reservation, ReservationResponseDto>();

            CreateMap<Seat, Seat>();
            CreateMap<SeatRequestDto, Seat>();
            CreateMap<Seat, SeatRequestDto>();

            CreateMap<SeatType, SeatType>();
            CreateMap<SeatTypeRequestDto, SeatType>();
            CreateMap<SeatType, SeatTypeResponseDto>();

            CreateMap<Sponsor, Sponsor>();
            CreateMap<SponsorRequestDto, Sponsor>();
            CreateMap<Sponsor, SponsorResponseDto>();

            CreateMap<TicketType, TicketType>();
            CreateMap<TicketTypeRequestDto, TicketType>();
            CreateMap<TicketType, TicketTypeResponseDto>();

            CreateMap<UserData, UserData>();
            CreateMap<UserDataRequestDto, UserData>();
            CreateMap<UserData, UserDataResponseDto>();

            CreateMap<User, User>();
            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
