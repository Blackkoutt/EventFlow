﻿using EventFlowAPI.DB.Entities;

namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoOptions
{
    public class HallRentInfoOptions(HallRent hallRent) : InfoOptions
    {
        private readonly HallRent _hallRent = hallRent;

        protected override string OrderIdLabel => "Numer wynajmu:";
        protected override string OrderId => $"{_hallRent.Id}";
        protected override string DateLabel => "Wynajem dokonany:";
        protected override string DateOfOrder => $"{_hallRent.RentDate.ToString(DateFormat.DateTimeFullMonth)}";
        protected override string TypeOfPayment => $"{_hallRent.PaymentType.Name}";
        protected sealed override string UserName => $"{_hallRent.User.Name}";
        protected sealed override string UserSurname => $"{_hallRent.User.Surname}";
        protected sealed override string UserEmail => $"{_hallRent.User.Email}";
    }
}
