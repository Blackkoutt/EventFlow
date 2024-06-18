﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventFlowAPI.DB.Models
{
    public class MediaPatron
    {
        public int Id { get; set; }

        [NotNull]
        [MaxLength(30)]
        public string? Name { get; set; }

        public ICollection<Festival_MediaPatron> Festivals { get; set; } = new List<Festival_MediaPatron>();
    }
}
