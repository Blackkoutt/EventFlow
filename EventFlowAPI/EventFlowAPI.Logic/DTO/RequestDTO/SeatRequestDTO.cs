using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class SeatRequestDto : IRequestDto
    {
        // Check if such seat number does not already exist in given room 

        [Required(ErrorMessage = "Numer miejsca jest wymagany.")]
        [Range(1, 625, ErrorMessage = "Numer miejsca nie może być mniejszy niż 1 lub większy niż 625.")]
        public int SeatNr { get; set; }

        [Range(1, 25, ErrorMessage = "Numer rzędu nie może być mniejszy niż 1 lub większy niż 25.")]
        [Required(ErrorMessage = "Numer rzędu jest wymagany.")]
        public int Row { get; set; }

        [Range(1, 25, ErrorMessage = "Numer rzędu w siatce nie może być mniejszy niż 1 lub większy niż 25.")]
        [Required(ErrorMessage = "Numer rzędu w siatce  jest wymagany.")]
        public int GridRow { get; set; }

        [Range(1, 25, ErrorMessage = "Numer kolumny nie może być mniejszy niż 1 lub większy niż 25.")]
        [Required(ErrorMessage = "Numer kolumny jest wymagany.")]
        public int Column { get; set; }

        [Range(1, 25, ErrorMessage = "Numer kolumny w siatce nie może być mniejszy niż 1 lub większy niż 25.")]
        [Required(ErrorMessage = "Numer kolumny w siatce jest wymagany.")]
        public int GridColumn { get; set; }

        [Required(ErrorMessage = "Należy podać typ miejsca.")]
        public int SeatTypeId { get; set; }
    }
}
