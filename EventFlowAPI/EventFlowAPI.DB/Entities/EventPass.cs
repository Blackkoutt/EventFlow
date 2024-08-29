﻿using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.DB.Entities
{
    public class EventPass : BaseEntity, IDateableEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }

        [Range(0.00, 9999.99),
         Column(TypeName = "NUMERIC(6,2)")]
        public decimal PaymentAmount { get; set; }
        public int PassTypeId { get; set; }
        public string UserId {  get; set; } = string.Empty;
        public int PaymentTypeId { get; set; }
        public EventPassType PassType { get; set; } = default!;
        public User User { get; set; } = default!;
        public PaymentType PaymentType { get; set; } = default!;
    }
}
