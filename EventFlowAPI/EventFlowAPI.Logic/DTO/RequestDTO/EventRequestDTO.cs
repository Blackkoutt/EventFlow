﻿using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    // Need to add custom validators to dates
    public class EventRequestDto : StartEndDateAbstract, IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa wydarzenia jest wymagana.")]
        [Length(2, 60, ErrorMessage = "Nazwa powinna zawierać od 2 do 60 znaków.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Krótki opis wydarzenia jest wymagany.")]
        [Length(2, 300, ErrorMessage = "Krótki opis powinien zawierać od 2 do 300 znaków.")]
        public string ShortDescription { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = "Opis powinnien zawierać mniej niż 2000 znaków.")]
        public string? LongDescription { get; set; }

        [Required(ErrorMessage = "Należy podać kategorię wydarzenia.")]
        [Range(0, int.MaxValue, ErrorMessage = "Id kategorii musi być większe lub równe 0.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Należy podać numer sali w którym odbywa się wydarzenie.")]
        [Range(0, int.MaxValue, ErrorMessage = "Id hali musi być większe lub równe 0.")]
        public int HallId { get; set; }

    }
}
