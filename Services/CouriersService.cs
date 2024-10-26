using CourierService.Enums;
using CourierService.Models;
using CourierService.Protos;
using CourierService.Repo;
using Grpc.Core;

namespace CourierService.Services
{
    public class CouriersService : Couriers.CouriersBase
    {
        private readonly ICourierRepo _courierRepo;

        public CouriersService(ICourierRepo courierRepo)
        {
            _courierRepo = courierRepo;
        }

        public override async Task<GetAllCouriersResponse> GetAllCouriers(GetAllCouriersRequest request, ServerCallContext context)
        {
            var couriers = await _courierRepo.GetAll();
            var response = new GetAllCouriersResponse();

            foreach (var courier in couriers)
            {
                var courierResponse = new CourierResponse
                {
                    Id = courier.Id,
                    Type = courier.Type.ToString(),
                    FullName = courier.FullName,
                    Dist = courier.Dist,
                };
                response.Couriers.Add(courierResponse);
            }

            return await Task.FromResult(response);
        }

        public override async Task<CourierResponse> CreateCourier(CreateCourierRequest request, ServerCallContext context)
        {
            if (!Enum.TryParse<TypeEnum>(request.Type, true, out var courierType))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid courier type: {request.Type}."));
            }

            var newCourier = new CourierModel
            {
                FullName = request.FullName,
                Dist = request.Dist,
                Type = courierType,
            };
            await _courierRepo.Create(newCourier);

            var response = new CourierResponse
            {
                Id = newCourier.Id,
                FullName = newCourier.FullName,
                Dist = newCourier.Dist,
                Type = newCourier.Type.ToString(),
            };

            return await Task.FromResult(response);
        }
    }
}
