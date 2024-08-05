using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    // rows and columns and max number of seats needs to be reduced 
    public class HallRequestDto : IRequestDto
    {
        // custom validator to verify that hall with given number does not exist
        [Required(ErrorMessage = "Numer sali jest wymagany.")]
        public int HallNr { get; set; }

        [Required(ErrorMessage = "Cena wynajmu sali jest wymagana.")]
        [Range(0.00, 999.99, ErrorMessage = "Cena nie może być mniejsza niż 0 lub większa niż 999.99.")]     
        public decimal RentalPricePerHour { get; set; }

        [Required(ErrorMessage = "Numer piętra na którym znajduje się sala jest wymagany.")]
        [Range(1,4, ErrorMessage = "Numer piętra nie może być mniejszy niż 0 lub większy niż 4.")]   
        public int Floor { get; set; }

        [Required(ErrorMessage = "Długość sali jest wymagana.")]
        [Range(4, 99.99, ErrorMessage = "Długość sali nie może być mniejsza niż 4 metry i większa niż 99.99 metrów.")]
        public decimal TotalLength { get; set; }

        [Required(ErrorMessage = "Szerokość sali jest wymagana.")]
        [Range(4, 99.99, ErrorMessage = "Szerokość sali nie może być mniejsza niż 4 metry i większa niż 99.99 metrów.")]
        public decimal TotalWidth { get; set; }

        [Required(ErrorMessage = "Powierzchnia sali jest wymagana.")]
        [Range(16.00, 999.99, ErrorMessage = "Powierzchnia sali nie może być mniejsza niż 16 m2 lub większa niż 999.99 m2.")]
        [HallAreaValidator]
        public decimal TotalArea { get; set; }

        [Range(10.00, 400.00, ErrorMessage = "Powierzchnia sceny nie może być mniejsza niż 10 m2 lub większa niż 400 m2.")]
        public decimal? StageArea { get; set; }

        [Required(ErrorMessage = "Ilość rzędów miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość rzędów miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        [HallRowsValidator]
        public int NumberOfSeatsRows { get; set; }

        [Required(ErrorMessage = "Ilość rzędów miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość rzędów miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        public int MaxNumberOfSeatsRows { get; set; }

        [Required(ErrorMessage = "Ilość kolumn miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość kolumn miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        [HallColumnsValidator]
        public int NumberOfSeatsColumns { get; set; }

        [Required(ErrorMessage = "Ilość kolumn miejsc w sali jest wymagana.")]
        [Range(1, 25, ErrorMessage = "Ilość kolumn miejsc w sali nie może być mniejsza niż 1 lub większa niż 25.")]
        public int MaxNumberOfSeatsColumns { get; set; }

        [Required(ErrorMessage = "Ilość miejsc w sali jest wymagana.")]
        [Range(5, 625, ErrorMessage = "Ilość miejsc w sali nie może być mniejsza niż 1 lub większa niż 625.")]
        [HallSeatsValidator]
        public int NumberOfSeats { get; set; }

        [Required(ErrorMessage = "Maksymalna ilość miejsc w sali jest wymagana.")]
        [Range(5, 625, ErrorMessage = "Maksymalna ilość miejsc w sali nie może być mniejsza niż 0 lub większa niż 625")]
        [HallMaxSeatsValidator]
        public int MaxNumberOfSeats { get; set; }

        [Required(ErrorMessage = "Typ sali jest wymagany.")]
        public int HallTypeId { get; set; }
    }
}
