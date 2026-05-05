namespace ECommerce.Application.DTOs;

public class AuthResponseDto //Lo que devuelve el servidor tras login/register exitoso — incluye el token JWT
{
    public string Token { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}