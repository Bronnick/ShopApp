using Orders.Api.DTOs;

namespace Orders.Api.Interfaces;

public interface ICatalogService
{
    Task<CatalogProductDto?> GetProductAsync(int id, CancellationToken ct = default);
}
