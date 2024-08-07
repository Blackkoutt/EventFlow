﻿using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class PaymentType
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public ICollection<HallRent> HallRents { get; set; } = [];
        public ICollection<EventPass> EventPasses { get; set; } = [];
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}
