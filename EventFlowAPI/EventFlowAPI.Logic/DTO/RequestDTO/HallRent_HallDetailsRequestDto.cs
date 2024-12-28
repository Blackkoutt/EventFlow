using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class HallRent_HallDetailsRequestDto : IRequestDto, IHallDetailsRequestDto
    {
        // input in frontend 
        [Range(3.00, 30.00, ErrorMessage = "Długość sceny nie może być mniejsza niż 3 m lub większa niż 30 m.")]
        public float? StageLength { get; set; }

        [Range(3.00, 30.00, ErrorMessage = "Szerokość sceny nie może być mniejsza niż 3 m lub większa niż 30 m.")]
        public float? StageWidth { get; set; }

        // calculated in frontend based on active seats
       /* [Required(ErrorMessage = "Ilość rzędów miejsc w sali jest wymagana.")]
        [Range(1, 20, ErrorMessage = "Ilość rzędów miejsc w sali nie może być mniejsza niż 1 lub większa niż 20.")]
        [HallRowsValidator]
        public int NumberOfSeatsRows { get; set; }*/

        // calculated in frontend based on legnth and width
        [Required(ErrorMessage = "Ilość rzędów miejsc w sali jest wymagana.")]
        [Range(1, 20, ErrorMessage = "Ilość rzędów miejsc w sali nie może być mniejsza niż 1 lub większa niż 20.")]
        public int MaxNumberOfSeatsRows { get; set; }

        // calculated in frontend based on active seats
        /*[Required(ErrorMessage = "Ilość kolumn miejsc w sali jest wymagana.")]
        [Range(1, 20, ErrorMessage = "Ilość kolumn miejsc w sali nie może być mniejsza niż 1 lub większa niż 20.")]
        [HallColumnsValidator]
        public int NumberOfSeatsColumns { get; set; }*/

        // calculated in frontend based on legnth and width
        [Required(ErrorMessage = "Ilość kolumn miejsc w sali jest wymagana.")]
        [Range(1, 20, ErrorMessage = "Ilość kolumn miejsc w sali nie może być mniejsza niż 1 lub większa niż 20.")]
        public int MaxNumberOfSeatsColumns { get; set; }
    }
}
