using CourierService.Data;
using CourierService.Repo;
using CourierService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    options.UseNpgsql(connectionString);
    Console.WriteLine($"Connection String: {connectionString}");
});
builder.Services.AddScoped<ICourierRepo, CourierRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();

        if (!dbContext.Couriers.Any())
        {
            var courierRepo = services.GetRequiredService<ICourierRepo>();
            await courierRepo.FillData();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

app.MapGrpcService<CouriersService>();
app.MapGrpcService<OrderService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
