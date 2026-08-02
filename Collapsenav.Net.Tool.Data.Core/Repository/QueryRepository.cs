using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;

public class QueryRepository<T> : Repository<T>, IQueryRepository<T> where T : class
{
    public QueryRepository(IDB db) : base(db) { }
    public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? exp = null) => await Task.FromResult(Query(exp).ToList());
    public virtual async Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>>? exp, PageRequest? page = null)
    {
        var query = Query(exp);
        page ??= new PageRequest();
        return new PageData<T>(await Task.FromResult(query.Skip(page.Skip).Take(page.Max).ToList()), await Task.FromResult(query.Count()));
    }
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids)
    {
        return await _db.FindByIdsAsync<T, TKey>(ids);
    }
    public virtual async Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>>? exp, Expression<Func<T, E>>? orderBy, bool isAsc = true, PageRequest? page = null)
    {
        var query = Query(exp);
        page ??= new PageRequest();
        if (orderBy != null)
            query = isAsc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return new PageData<T>(await Task.FromResult(query.Skip(page.Skip).Take(page.Max).ToList()), await Task.FromResult(query.Count()));
    }

    public virtual async Task<T?> GetByIdAsync<TKey>(TKey? id)
    {
        return await _db.FindByIdAsync<T, TKey>(id);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? exp = null)
    {
        return await Task.FromResult(Query(exp).Count());
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>>? exp)
    {
        return await Task.FromResult(Query(exp).Any());
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? exp)
    {
        return await Task.FromResult(Query(exp).FirstOrDefault());
    }
}