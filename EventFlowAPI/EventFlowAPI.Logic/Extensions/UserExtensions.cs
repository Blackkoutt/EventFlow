using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Identity.Helpers;

namespace EventFlowAPI.Logic.Extensions
{
    public static class UserExtensions
    {
        public static bool IsInRole(this UserResponseDto user, Roles role) => 
            user.UserRoles.Contains(role.ToString());
    }
}
