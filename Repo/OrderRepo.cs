using CourierService.Data;
using CourierService.Models;

namespace CourierService.Repo
{
    public interface IOrderRepo : IRepo<OrderModel>
    {
    }

    public class OrderRepo : Repo<OrderModel>, IOrderRepo
    {
        public OrderRepo(AppDbContext context) : base(context) { }
    }
}
