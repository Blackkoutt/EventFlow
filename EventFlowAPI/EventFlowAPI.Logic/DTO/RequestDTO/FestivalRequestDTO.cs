﻿using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class FestivalRequestDto : IRequestDto, INameableRequestDto
    {
        [Required(ErrorMessage = "Nazwa festiwalu jest wymagana.")]
        [Length(2, 60, ErrorMessage = "Nazwa powinna zawierać od 2 do 60 znaków.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Krótki opis festiwalu jest wymagany.")]

        //[Length(2, 300, ErrorMessage = "Krótki opis powinien zawierać od 2 do 300 znaków.")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wybierz co najmniej jedno wydarzenie.")]
        public List<int> EventIds { get; set; } = [];

        [Required(ErrorMessage = "Wybierz co najmniej jednego patrona medialnego.")]
        public List<int> MediaPatronIds { get; set; } = [];

        [Required(ErrorMessage = "Wybierz co najmniej jednego organizatora.")]
        public List<int> OrganizerIds { get; set; } = [];

        [Required(ErrorMessage = "Wybierz co najmniej jednego sponsora.")]
        public List<int> SponsorIds { get; set; } = [];
        public FestivalDetailsRequestDto? Details { get; set; } = default!;

        [MaxFileSizeValidator(10)]
        public IFormFile? FestivalPhoto { get; set; }

        public ICollection<Event_FestivalTicketRequestDto> FestivalTickets { get; set; } = [];
    }
}
