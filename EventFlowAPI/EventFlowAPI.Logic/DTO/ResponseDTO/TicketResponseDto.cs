﻿using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class TicketResponseDto : BaseResponseDto
    {
        public decimal Price { get; set; }
        //public EventResponseDto? Event { get; set; }
        public TicketTypeResponseDto? TicketType { get; set; }
    }
}