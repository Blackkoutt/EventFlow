using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.UpdateRequestDto
{
    public class UpdateNewsRequestDto : IRequestDto, ITitleableRequestDto
    {
        [Required(ErrorMessage = "Tytuł artykułu jest wymagany.")]
        [Length(2, 60, ErrorMessage = "Tytuł artykułu powinien zawierać od 2 do 60 znaków.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Krótka treść artykułu jest wymagana.")]
        [Length(2, 300, ErrorMessage = "Krótka treść artykułu powinna zawierać od 2 do 300 znaków.")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Długa treść artykułu jest wymagana.")]
        [Length(2, 2000, ErrorMessage = "Długa treść artykułu powinna zawierać od 2 do 2000 znaków.")]
        public string LongDescription { get; set; } = string.Empty;

        [MaxFileSizeValidator(10)]
        public IFormFile? NewsPhoto { get; set; }
    }
}
