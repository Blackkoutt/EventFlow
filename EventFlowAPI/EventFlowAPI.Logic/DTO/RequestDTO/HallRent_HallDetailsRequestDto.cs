using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class HallRent_HallDetailsRequestDto : IRequestDto, IHallDetailsRequestDto
    {
        // input in frontend 
        [Range(10.00, 400.00, ErrorMessage = "Powierzchnia sceny nie może być mniejsza niż 10 m2 lub większa niż 400 m2.")]
        public decimal? StageArea { get; set; }

        // calculated in frontend based on active seats
        [Required(ErrorMessage = "Ilość rzędów miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość rzędów miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        [HallRowsValidator]
        public int NumberOfSeatsRows { get; set; }

        // calculated in frontend based on legnth and width
        [Required(ErrorMessage = "Ilość rzędów miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość rzędów miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        public int MaxNumberOfSeatsRows { get; set; }

        // calculated in frontend based on active seats
        [Required(ErrorMessage = "Ilość kolumn miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość kolumn miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        [HallColumnsValidator]
        public int NumberOfSeatsColumns { get; set; }

        // calculated in frontend based on legnth and width
        [Required(ErrorMessage = "Ilość kolumn miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość kolumn miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        public int MaxNumberOfSeatsColumns { get; set; }
    }
}
