namespace EventFlowAPI.Logic.DTO.ResponseDto
{
    public class UserResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public UserDataResponseDto? UserData { get; set; }
        public IList<string?> UserRoles { get; set; } = [];
    }
}
