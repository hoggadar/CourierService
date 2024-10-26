using CourierService.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CourierModel> Couriers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
