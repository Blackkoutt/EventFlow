using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [Required(ErrorMessage = "Szczegóły dotyczące sali są wymagane.")]
        public HallDetailsRequestDto HallDetails { get; set; } = default!;

        [Required(ErrorMessage = "Typ sali jest wymagany.")]
        public int HallTypeId { get; set; }

        [JsonIgnore]
        public bool IsCopy { get; set; }

        [JsonIgnore]
        public bool IsVisible { get; set; }

        public ICollection<SeatRequestDto> Seats { get; set; } = [];
    }
}
