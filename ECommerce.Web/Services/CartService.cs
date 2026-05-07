namespace ECommerce.Web.Services;

using ECommerce.Web.Models;

public class CartService
{
    // El carrito es una lista simple
    private readonly List<CartItem> _items = new();

    // Notifica cuando el carrito cambia
    public event Action? OnCartChanged;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public int TotalItems => _items.Sum(i => i.Quantity);

    public decimal Total => _items.Sum(i => i.Subtotal);

    public void AddItem(ProductModel product, int quantity = 1)
    {
        // Busca si el producto ya está en el carrito
        var existing = _items.FirstOrDefault(i => i.ProductId == product.Id);

        if (existing is not null)
        {
            // Si ya existe aumenta la cantidad
            existing.Quantity += quantity;
        }
        else
        {
            // Si no existe agrega un nuevo item
            _items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity
            });
        }

        // Notifica que el carrito cambió
        OnCartChanged?.Invoke();
    }

    public void RemoveItem(int productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            _items.Remove(item);
            OnCartChanged?.Invoke();
        }
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            if (quantity <= 0)
                _items.Remove(item);
            else
                item.Quantity = quantity;

            OnCartChanged?.Invoke();
        }
    }

    public void Clear()
    {
        _items.Clear();
        OnCartChanged?.Invoke();
    }
}