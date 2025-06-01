using Microsoft.EntityFrameworkCore;
using SeatBooking.Domain.IRepositories;
using SeatBooking.Infrastructure.Persistance.DbContext;

namespace SeatBooking.Infrastructure.Persistance.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    protected readonly SeatBookingDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(SeatBookingDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual Task AddRangeAsync(List<TEntity> entity, int batchSize = 10000)
    {
        if (entity == null || entity.Count == 0)
            return Task.CompletedTask;

        for (int i = 0; i < entity.Count; i += batchSize)
        {
            var batch = entity.Skip(i).Take(batchSize).ToList();
            _dbSet.AddRange(batch);
        }

        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }
    public virtual async Task UpdateRangeAsync(List<TEntity> entities, int batchSize = 10000)
    {
        if (entities == null || entities.Count == 0)
            return;

        for (int i = 0; i < entities.Count; i += batchSize)
        {
            var batch = entities.Skip(i).Take(batchSize).ToList();
            _dbSet.UpdateRange(batch);
            await _context.SaveChangesAsync();
        }
    }
}