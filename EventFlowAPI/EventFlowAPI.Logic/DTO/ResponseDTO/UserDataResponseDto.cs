using EventFlowAPI.Logic.DTO.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class UserDataResponseDto : BaseResponseDto
    {
        public string Street { get; set; } = string.Empty;
        public int HouseNumber { get; set; }
        public int? FlatNumber { get; set; }
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        //public UserResponseDto? User { get; set; } 
    }
}
