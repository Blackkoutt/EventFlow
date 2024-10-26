﻿using EventFlowAPI.DB.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.DB.Entities
{
    public class Sponsor : BaseEntity, INameableEntity
    {

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Festival> Festivals { get; set; } = [];
    }
}
