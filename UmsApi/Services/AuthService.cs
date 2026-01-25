using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using UmsApi.Data;
using UmsApi.DTOs;
using UmsApi.DTOs.Auth;
using UmsApi.DTOs.User;
using UmsApi.Models;
using UmsApi.Repositories;

namespace UmsApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        AppDbContext context,
        IJwtService jwtService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _context = context;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.Query()
            .FirstOrDefaultAsync(u => u.Email == request.Email && !u.Deleted);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas");
        }

        // Verificar password usando BCrypt
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas");
        }

        // Registrar inicio de sesión
        var sessionLog = new SessionLog
        {
            UserId = user.Id,
            StartDate = DateTime.UtcNow
        };
        _context.SessionLogs.Add(sessionLog);
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user);
        var expiresAt = DateTime.UtcNow.AddHours(
            int.Parse(_configuration["JWT:TokenExpiryTimeInHour"] ?? "1")
        );

        return new LoginResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Studies = new List<StudyDto>(),
                Addresses = new List<AddressDto>()
            },
            ExpiresAt = expiresAt
        };
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Verificar si el email ya existe
        var existingUser = await _userRepository.Query()
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException("El email ya está registrado");
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role,
            CreatedAt = DateTime.UtcNow
        };

        user = await _userRepository.AddAsync(user);

        return new RegisterResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task LogoutAsync(long userId)
    {
        var activeSession = await _context.SessionLogs
            .Where(s => s.UserId == userId && s.EndDate == null)
            .OrderByDescending(s => s.StartDate)
            .FirstOrDefaultAsync();

        if (activeSession != null)
        {
            activeSession.EndDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}

