﻿using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class PaymentTypeResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
