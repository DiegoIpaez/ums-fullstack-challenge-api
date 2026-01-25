using System.Security.Claims;

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

    public static string GetUserRole(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Role)?.Value ?? "User";
    }

    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user.GetUserRole() == "Admin";
    }
}

