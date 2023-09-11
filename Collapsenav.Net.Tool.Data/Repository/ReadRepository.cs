using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class ReadRepository<T> : Repository<T>, IReadRepository<T> where T : class, IEntity
{
    public ReadRepository(DbContext db) : base(db) { }

    public virtual async Task<T?> GetByIdAsync<TKey>(TKey? id)
    {
        if (id == null)
            return null;
        if (typeof(TKey) == KeyType())
            return await dbSet.FindAsync(id);
        return await dbSet.FindAsync(GetKeyValue(id));
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
