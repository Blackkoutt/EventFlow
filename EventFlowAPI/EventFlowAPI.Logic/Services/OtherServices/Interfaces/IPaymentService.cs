﻿using EventFlowAPI.Logic.Helpers.PayU;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IPaymentService
    {
        Task<Result<TRequestDto>> CheckNewTransactionStatus<TRequestDto>(string key) where TRequestDto : class;
        Task<Result<PayUCreatePaymentResponseDto>> CreatePayment(PayURequestPaymentDto requestDto);
        Task<Result<PayUTransactionStatusDto>> GetTransactionStatus(string transactionId);
        Task<Result<object>> CheckTransactionStatus();
    }
}
