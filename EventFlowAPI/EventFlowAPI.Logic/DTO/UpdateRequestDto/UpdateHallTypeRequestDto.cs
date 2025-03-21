﻿using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateHallTypeRequestDto : IRequestDto, INameableRequestDto
    {

        [Required(ErrorMessage = "Nazwa typu sali jest wymagana.")]
        [Length(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(600, ErrorMessage = "Opis powinien  zawierać mniej niż 600 znaków.")]
        public string? Description { get; set; }

        public List<int> EquipmentIds { get; set; } = [];

        [MaxFileSizeValidator(10)]
        public IFormFile? HallTypePhoto { get; set; }

    }
}
