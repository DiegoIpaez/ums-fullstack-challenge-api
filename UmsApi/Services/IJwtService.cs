using System.Security.Claims;
using UmsApi.Models;

namespace UmsApi.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
}

