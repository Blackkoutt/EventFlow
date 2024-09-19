using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class HallDetailsRequestDto : IRequestDto
    {
        [Required(ErrorMessage = "Długość sali jest wymagana.")]
        [Range(4, 99.99, ErrorMessage = "Długość sali nie może być mniejsza niż 4 metry i większa niż 99.99 metrów.")]
        public decimal TotalLength { get; set; }

        [Required(ErrorMessage = "Szerokość sali jest wymagana.")]
        [Range(4, 99.99, ErrorMessage = "Szerokość sali nie może być mniejsza niż 4 metry i większa niż 99.99 metrów.")]
        public decimal TotalWidth { get; set; }

        // calculated in frontend based on width and length
        [Required(ErrorMessage = "Powierzchnia sali jest wymagana.")]
        [Range(16.00, 999.99, ErrorMessage = "Powierzchnia sali nie może być mniejsza niż 16 m2 lub większa niż 999.99 m2.")]
        [HallAreaValidator]
        public decimal TotalArea { get; set; }

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
