using Catalog.Api.Entities;
using Catalog.Api.Interfaces;

namespace Catalog.Api.Data;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products =
    [
        new Product { Id = 1, Name = "Keyboard", Price = 49.99m, Stock = 10 },
        new Product { Id = 2, Name = "Mouse", Price = 19.99m, Stock = 0 },
        new Product { Id = 3, Name = "Monitor", Price = 199.00m, Stock = 3 }
    ];

    public Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return Task.FromResult<IReadOnlyList<Product>>(_products);
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        return Task.FromResult(_products.SingleOrDefault(product => product.Id == id));
    }
}
