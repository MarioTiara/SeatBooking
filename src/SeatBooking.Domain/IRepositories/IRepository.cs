namespace SeatBooking.Domain.IRepositories;


public interface IRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(List<TEntity> entity, int batchSize = 10000);
    Task UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(List<TEntity> entity, int batchSize = 10000);
    Task DeleteAsync(TKey id);
    Task SaveChangesAsync();
}
