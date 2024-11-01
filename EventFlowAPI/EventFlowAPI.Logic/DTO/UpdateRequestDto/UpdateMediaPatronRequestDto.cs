﻿using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateMediaPatronRequestDto : IRequestDto, INameableRequestDto
    {
        [Required(ErrorMessage = "Nazwa patrona medialnego jest wymagana.")]
        [Length(2, 50, ErrorMessage = "Nazwa powinna zawierać od 2 do 50 znaków.")]
        public string Name { get; set; } = string.Empty;

        [MaxFileSizeValidator(10)]
        public IFormFile? MediaPatronPhoto { get; set; }
    }
}
