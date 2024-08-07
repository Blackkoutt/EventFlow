﻿using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class EventDetails
    {
        public int Id { get; set; }

        [MaxLength(2000)]
        public string? LongDescription { get; set; }

        public Event? Event { get; set; }
    }
}
