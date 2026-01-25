using System.Security.Claims;
using UmsApi.Models.Enums;

namespace UmsApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return long.TryParse(userIdClaim, out var userId) ? userId : 0;
    }

    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public static UserRole GetUserRole(this ClaimsPrincipal user)
    {
        var roleValue = user.FindFirst(ClaimTypes.Role)?.Value;
        if (Enum.TryParse<UserRole>(roleValue, out var role))
            return role;
        return UserRole.User;
    }

    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user.GetUserRole() == UserRole.Admin;
    }
}
