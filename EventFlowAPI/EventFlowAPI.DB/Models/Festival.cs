﻿using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Models
{
    public class Festival
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public FestivalDetails? Details { get; set; }
        public ICollection<Festival_Event> Events { get; set; } = new List<Festival_Event>();
        public ICollection<Festival_MediaPatron> MediaPatrons { get; set; } = new List<Festival_MediaPatron>();
        public ICollection<Festival_Organizer> Organizers { get; set; } = new List<Festival_Organizer>();
        public ICollection<Festival_Sponsor> Sponsors { get; set; } = new List<Festival_Sponsor>();
    }
}
