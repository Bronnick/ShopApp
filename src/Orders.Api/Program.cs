using Orders.Api.Data;
using Orders.Api.Interfaces;
using Orders.Api.Middleware;
using Orders.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>(client =>
{
    var catalogUrl = builder.Configuration["Services:Catalog"]
        ?? throw new Exception("Catalog url not configured - Program.cs");
    client.BaseAddress = new Uri(catalogUrl);
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
