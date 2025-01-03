﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class EventResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string PhotoName { get; set; } = string.Empty;
        public string PhotoEndpoint { get; set; } = string.Empty;
        public EventCategoryResponseDto? Category { get; set; }
        public EventDetailsResponseDto? Details { get; set; }
        public HallResponseDto? Hall { get; set; }
        public ICollection<TicketResponseDto> Tickets { get; set; } = [];
    }
}
