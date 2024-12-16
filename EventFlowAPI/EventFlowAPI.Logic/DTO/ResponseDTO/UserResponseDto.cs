using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class UserResponseDto : IUser
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
       // public string Email { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhotoName { get; set; } = string.Empty;
        public string PhotoEndpoint { get; set; } = string.Empty;
        public UserDataResponseDto? UserData { get; set; }
        public IList<string?> UserRoles { get; set; } = [];
    }
}
