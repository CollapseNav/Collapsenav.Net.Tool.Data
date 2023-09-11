using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class ReadRepository<T> : Repository<T>, IReadRepository<T> where T : class, IEntity
{
    public ReadRepository(DbContext db) : base(db) { }

    public virtual async Task<T?> GetByIdAsync<TKey>(TKey? id)
    {
        if (typeof(TKey) == KeyType())
            return await dbSet.FindAsync(id);
        return KeyType().Name! switch
        {
            nameof(Int32) => await dbSet.FindAsync(int.Parse(id?.ToString() ?? string.Empty)),
            nameof(Int64) => await dbSet.FindAsync(long.Parse(id?.ToString() ?? string.Empty)),
            nameof(String) => await dbSet.FindAsync(id?.ToString() ?? string.Empty),
            nameof(Guid) => await dbSet.FindAsync(Guid.Parse(id?.ToString() ?? string.Empty)),
            _ => await dbSet.FindAsync(id?.ToString() ?? string.Empty),
        };
    }
    public virtual async Task<IEnumerable<T>> QueryDataAsync(IQueryable<T>? query) => query == null ? Enumerable.Empty<T>() : await query.ToListAsync();
    public virtual async Task<IEnumerable<T>> QueryAsync(IQueryable<T>? query) => query == null ? Enumerable.Empty<T>() : await query.ToListAsync();
    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>>? exp) => exp == null ? await dbSet.AnyAsync() : await dbSet.AnyAsync(exp);
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? exp = null) => await Query(exp).CountAsync();

    public virtual async Task<PageData<T>> QueryPageAsync(IQueryable<T>? query, PageRequest? page = null)
    {
        page ??= new PageRequest();
        return new PageData<T>(query == null ? null : await query.Skip(page.Skip).Take(page.Max).ToListAsync(), query == null ? null : await query.CountAsync());
    }

    public virtual async Task<PageData<ReturnT>> QueryPageAsync<ReturnT>(IQueryable<ReturnT>? query, PageRequest? page = null)
    {
        page ??= new PageRequest();
        return new PageData<ReturnT>(query == null ? null : await query.Skip(page.Skip).Take(page.Max).ToListAsync(), query == null ? null : await query.CountAsync());
    }
}
