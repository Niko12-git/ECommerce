namespace ECommerce.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public int RoleId { get; private set; }
    public Role? Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public User(string name, string email, string passwordHash, int roleId)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash; //Guardamos la versión encriptada de la contraseña (hash)
        RoleId = roleId;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;  //En lugar de borrar usuarios se desactivan (soft delete)
    }

    public void UpdateName(string name) => Name = name;

    public void UpdateEmail(string email) => Email = email;

    public void UpdatePasswordHash(string passwordHash)
        => PasswordHash = passwordHash;

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;

    // Constructor vacío requerido por EF
    private User()
    {
        Name = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
    }
}