using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class NoConstraintsQueryRepository<Context, T> : NoConstraintsQueryRepository<T>, INoConstraintsQueryRepository<Context, T> where T : class where Context : DbContext
{
    public NoConstraintsQueryRepository(Context db) : base(db) { }
}
public class NoConstraintsQueryRepository<T> : NoConstraintsRepository<T>, INoConstraintsQueryRepository<T> where T : class
{
    public NoConstraintsQueryRepository(DbContext db) : base(db) { }
    public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? exp = null) => await Query(exp).ToListAsync();
    public virtual async Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>>? exp, PageRequest? page = null)
    {
        var query = Query(exp);
        page ??= new PageRequest();
        return new PageData<T>(await query.Skip(page.Skip).Take(page.Max).ToListAsync(), await query.CountAsync());
    }
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids)
    {
        if (ids == null)
            return Enumerable.Empty<T>();
        List<T> results = new();
        foreach (var item in ids)
        {
            var data = await GetByIdAsync(item);
            if (data == null)
                continue;
            results.Add(data);
        }
        return results;
    }
    public virtual async Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>>? exp, Expression<Func<T, E>>? orderBy, bool isAsc = true, PageRequest? page = null)
    {
        var query = Query(exp);
        page ??= new PageRequest();
        if (orderBy != null)
            query = isAsc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return new PageData<T>(await query.Skip(page.Skip).Take(page.Max).ToListAsync(), await query.CountAsync());
    }

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
    public virtual async Task<IEnumerable<E>> QueryAsync<E>(IQueryable<E>? query)
    {
        return query == null ? Enumerable.Empty<E>() : await query.ToListAsync();
    }
}