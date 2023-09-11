using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class QueryRepository<T> : ReadRepository<T>, IQueryRepository<T> where T : class, IEntity
{
    public QueryRepository(DbContext db) : base(db) { }
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
}