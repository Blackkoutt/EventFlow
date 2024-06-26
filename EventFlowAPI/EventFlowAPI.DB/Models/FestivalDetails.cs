﻿using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class FestivalDetails
    {
        public int Id { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }
        public Festival? Festival { get; set; }
    }
}
