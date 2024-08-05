﻿using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class User : BaseEntity
    {

        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(40)]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public UserData? UserData { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = [];
        public ICollection<EventPass> EventPasses { get; set; } = [];
        public ICollection<HallRent> HallRents { get; set; } = [];
    }
}
