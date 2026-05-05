namespace ECommerce.Application.DTOs;

public class UpdateUserDto //Datos que se pueden modificar de un usuario
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RoleId { get; set; }
}