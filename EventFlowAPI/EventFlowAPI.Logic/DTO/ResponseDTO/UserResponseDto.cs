using EventFlowAPI.Logic.DTO.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class UserResponseDto : BaseResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public UserDataResponseDto? UserData { get; set; }
        public ICollection<ReservationResponseDto> Reservations { get; set; } = [];
        public ICollection<EventPassResponseDto> EventPasses { get; set; } = [];
        public ICollection<HallRentResponseDto> HallRents { get; set; } = [];
    }
}
