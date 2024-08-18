﻿using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class Hall : BaseEntity
    {
        public int HallNr { get; set; }

        [Range(0.00, 999.99),
         Column(TypeName = "NUMERIC(5,2)")]
        public decimal RentalPricePerHour { get; set; }

        public bool IsCopy { get; set; } = false;

        public bool IsVisible { get; set; } = true;

        [Range(0,4),
         Column(TypeName = "NUMERIC(1)")]
        public int Floor { get; set; }

        [Range(0.00, 99.99), 
         Column(TypeName ="NUMERIC(4,2)")]
        public decimal TotalLength { get; set; }

        [Range(0.00, 99.99),
         Column(TypeName = "NUMERIC(4,2)")]
        public decimal TotalWidth { get; set; }

        [Range(0.00, 999.99),
         Column(TypeName = "NUMERIC(5,2)")]
        public decimal TotalArea { get; set; }

        [Range(10.00, 400.00),
            Column(TypeName = "NUMERIC(5,2)")]
        public decimal? StageArea { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int NumberOfSeatsRows { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int MaxNumberOfSeatsRows { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int NumberOfSeatsColumns { get; set; }

        [Range(0, 99),
         Column(TypeName = "NUMERIC(2)")]
        public int MaxNumberOfSeatsColumns { get; set; }

        [Range(0,999),
            Column(TypeName ="NUMERIC(3)")]
        public int NumberOfSeats { get; set; }

        [Range(0, 999),
         Column(TypeName = "NUMERIC(3)")]
        public int MaxNumberOfSeats { get; set; }
        public int HallTypeId { get; set; }

        public HallType Type { get; set; } = default!;

        public ICollection<Event> Events { get; set; } = [];
        public ICollection<Seat> Seats { get; set; } = [];
        public ICollection<HallRent> Rents { get; set; } = [];
    }
}
