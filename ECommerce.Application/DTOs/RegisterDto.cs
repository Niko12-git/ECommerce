namespace ECommerce.Application.DTOs;

public class RegisterDto //Datos que el usuario envía al registrarse
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
}