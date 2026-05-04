namespace ECommerce.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    // Los setters son private para proteger el estado.
    // Solo métodos del dominio pueden cambiar el objeto — esto es encapsulamiento.
    
    public Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException("Stock insuficiente");
        Stock -= quantity;
    }
}