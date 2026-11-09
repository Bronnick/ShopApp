using System.Net;
using Orders.Api.DTOs;
using Orders.Api.Interfaces;

namespace Orders.Api.Services;

public class CatalogService(HttpClient http) : ICatalogService
{
    public async Task<CatalogProductDto?> GetProductAsync(int id, CancellationToken ct = default)
    {
        var catalogProduct = await http.GetAsync($"api/products/{id}");
        switch(catalogProduct.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return null;

            case HttpStatusCode.OK:
                var json = await catalogProduct.Content.ReadFromJsonAsync<CatalogProductDto>(ct);
                return json;

            default:
                throw new Exception();
        }
    }
}
