﻿using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class SeatResponseDto : BaseResponseDto
    {
        public int SeatNr { get; set; }
        public int Row { get; set; }
        public int GridRow { get; set; }
        public int Column { get; set; }     
        public bool IsAvailable { get; set; }
        public int GridColumn { get; set; }
        public SeatTypeResponseDto? SeatType { get; set; }
    }
}
