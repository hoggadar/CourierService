using CourierService.Data;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Repo
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity?> GetById(int id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
    }

    public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;

        public Repo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var created = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var updated = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            var deleted = _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return deleted.Entity;
        }
    }
}
