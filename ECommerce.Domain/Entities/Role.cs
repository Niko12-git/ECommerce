namespace ECommerce.Domain.Entities;

public class Role
{
    public int Id { get; private set; } //private set porque el nombre del rol no debe cambiarse desde afuera directamente
    public string Name { get; private set; }

    public Role(string name)
    {
        Name = name;
    }

    // Constructor vacío requerido por EF Core internamente para reconstruir el objeto desde la base de datos
    private Role() { Name = string.Empty; }
}