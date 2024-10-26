using CourierService.Models;
using CourierService.Protos;
using CourierService.Repo;
using Grpc.Core;

namespace CourierService.Services
{
    public class OrderService : Order.OrderBase
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public override async Task<GetAllOrdersResponse> GetAllOrders(GetAllOrdersRequest request, ServerCallContext context)
        {
            var orders = await _orderRepo.GetAll();
            var response = new GetAllOrdersResponse();

            foreach (var order in orders)
            {
                var orderResponse = new OrderResponse
                {
                    Id = order.Id,
                    Price = order.Price,
                    OverPrice = order.OverPrice,
                    CourierId = order.CourierId,
                };
                response.Orders.Add(orderResponse);
            }

            return await Task.FromResult(response);
        }

        public override async Task<OrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            var newOrder = new OrderModel
            {
                Desc = request.Desc,
                Price = request.Price,
                OverPrice = request.OverPrice,
                Date = DateTime.UtcNow,
                CourierId = request.CourierId,
            };
            await _orderRepo.Create(newOrder);

            var response = new OrderResponse
            {
                Id = newOrder.Id,
                Desc = newOrder.Desc,
                Price = newOrder.Price,
                OverPrice = newOrder.OverPrice,
                Date = newOrder.Date.ToString(),
                CourierId = newOrder.CourierId,
            };

            return await Task.FromResult(response);
        }

        public override async Task<OrderResponse> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            var order = await _orderRepo.GetById(request.Id);
            if (order == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Order not found!"));
            }

            await _orderRepo.Delete(order);

            var response = new OrderResponse
            {
                Id = order.Id,
                Desc = order.Desc,
                Price = order.Price,
                OverPrice = order.OverPrice,
                Date = order.Date.ToString(),
                CourierId= order.CourierId,
            };

            return await Task.FromResult(response);
        }
    }
}
