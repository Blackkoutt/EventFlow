using EventFlowAPI.Logic.DTO.ResponseDto;

namespace EventFlowAPI.Logic.Extensions
{
    public static class UserExtensions
    {
        public static bool IsInRole(this UserResponseDto user, string role) => 
            user.UserRoles.Contains(role);
    }
}
