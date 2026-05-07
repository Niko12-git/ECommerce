using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepo,
        IRoleRepository roleRepo,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        // Verificar que el email no esté ya registrado
        if (await _userRepo.ExistsAsync(dto.Email))
            throw new InvalidOperationException("El email ya está registrado");

        // Verificar que el rol existe
        var role = await _roleRepo.GetByIdAsync(dto.RoleId)
            ?? throw new InvalidOperationException("El rol especificado no existe");

        // Crear el usuario con password hasheado (nunca en texto plano)
        var user = new User(dto.Name, dto.Email, string.Empty, dto.RoleId);
        var hash = _passwordHasher.HashPassword(user, dto.Password);
        user.UpdatePasswordHash(hash);

        await _userRepo.AddAsync(user);

        var token = _jwtService.GenerateToken(user, role.Name);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Token = token,
            Name = user.Name,
            Email = user.Email,
            Role = role.Name,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        // Buscar usuario por email
        var user = await _userRepo.GetByEmailAsync(dto.Email)
            ?? throw new InvalidOperationException("Email o contraseña incorrectos");

        if (!user.IsActive)
            throw new InvalidOperationException("Usuario desactivado. Contacta al administrador");

        // Verificar password contra el hash guardado
        var result = _passwordHasher.VerifyHashedPassword(
            user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new InvalidOperationException("Email o contraseña incorrectos");

        // Cargar el rol para incluirlo en el token
        var role = await _roleRepo.GetByIdAsync(user.RoleId)
            ?? throw new InvalidOperationException("Rol no encontrado");

        var token = _jwtService.GenerateToken(user, role.Name);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Token = token,
            Name = user.Name,
            Email = user.Email,
            Role = role.Name,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepo.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt
        });
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user is null) return null;

        var role = await _roleRepo.GetByIdAsync(user.RoleId);

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = role?.Name ?? "Sin rol",
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepo.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Usuario no encontrado");

        // Verifica que el nuevo email no lo tenga otro usuario
        if (user.Email != dto.Email && await _userRepo.ExistsAsync(dto.Email))
            throw new InvalidOperationException("El email ya está en uso");

        user.UpdateName(dto.Name);
        user.UpdateEmail(dto.Email);

        await _userRepo.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Usuario no encontrado");

        user.Deactivate();
        await _userRepo.UpdateAsync(user);
    }

    public async Task ActivateUserAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Usuario no encontrado");

        user.Activate();
        await _userRepo.UpdateAsync(user);
    }
    
}