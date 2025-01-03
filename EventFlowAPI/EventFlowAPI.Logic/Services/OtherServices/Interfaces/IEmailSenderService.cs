﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IEmailSenderService
    {
        Task<Error> SendVerificationEmail(string userEmail, string name, string activationLink);
        Task<Error> SendInfo<TEntity>(TEntity entity, EmailType emailType, string userEmail, byte[]? attachmentData = null);
        Task SendEmailAsync(EmailDto emailDto);
        Task SendUpdatedTicketsAsync<TEntity>(List<(Reservation, byte[])> tupleList, TEntity? oldEntity, TEntity? newEntity) where TEntity : class;
        Task SendUpdatedHallRentsAsync(List<(HallRent, byte[])> tupleList, Hall oldHall);
        Task SendInfoAboutCanceledHallRents(List<HallRent> hallRentsToDelete);
        Task SendInfoAboutCanceledEvents(List<(Reservation, bool)> deleteReservationsInfo, Event? eventEntity = null, Festival? festival = null);
    }
}
