using CourierService.Data;
using CourierService.Enums;
using CourierService.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Repo
{
    public interface ICourierRepo : IRepo<CourierModel>
    {
        Task<CourierModel?> GetByFullName(string fullName);
        Task FillData();
    }

    public class CourierRepo : Repo<CourierModel>, ICourierRepo
    {
        public CourierRepo(AppDbContext context) : base(context) {}

        public async Task<CourierModel?> GetByFullName(string fullName)
        {
            return await _context.Couriers.FirstOrDefaultAsync(c => c.FullName == fullName);
        }

        public async Task FillData()
        {
            var couriers = new List<CourierModel>
            {
                new CourierModel { FullName = "Иванов Иван Иванович", Dist = 5, Type = TypeEnum.Auto },
                new CourierModel { FullName = "Петров Петр Петрович", Dist = 3, Type = TypeEnum.Velo },
                new CourierModel { FullName = "Сидоров Сидор Сидорович", Dist = 7, Type = TypeEnum.Peshiy },
                new CourierModel { FullName = "Кузнецов Николай Николаевич", Dist = 9, Type = TypeEnum.Auto },
                new CourierModel { FullName = "Смирнов Алексей Алексеевич", Dist = 2, Type = TypeEnum.Velo },
                new CourierModel { FullName = "Попов Сергей Сергеевич", Dist = 4, Type = TypeEnum.Auto },
                new CourierModel { FullName = "Васильев Михаил Васильевич", Dist = 6, Type = TypeEnum.Peshiy },
                new CourierModel { FullName = "Зайцев Юрий Юрьевич", Dist = 1, Type = TypeEnum.Velo },
                new CourierModel { FullName = "Морозов Антон Антонович", Dist = 8, Type = TypeEnum.Peshiy },
                new CourierModel { FullName = "Федоров Вадим Владимирович", Dist = 11, Type = TypeEnum.Peshiy }
            };

            await _context.Couriers.AddRangeAsync(couriers);
            await _context.SaveChangesAsync();
        }
    }
}
