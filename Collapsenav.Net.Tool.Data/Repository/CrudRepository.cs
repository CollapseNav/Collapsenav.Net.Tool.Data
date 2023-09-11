using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class CrudRepository<T> : Repository<T>, ICrudRepository<T> where T : class, IEntity, new()
{
    protected readonly IQueryRepository<T> Read;
    protected readonly IModifyRepository<T> Write;
    public CrudRepository(DbContext db) : base(db)
    {
        Read = new QueryRepository<T>(db);
        Write = new ModifyRepository<T>(db);
    }
    public virtual async Task<int> AddAsync(IEnumerable<T>? entityList) => await Write.AddAsync(entityList);
    public virtual async Task<T?> AddAsync(T? entity) => await Write.AddAsync(entity);
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? exp) => await Read.CountAsync(exp);
    public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>>? exp, bool isTrue = false) => await Write.DeleteAsync(exp, isTrue);
    public virtual async Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>>? exp, PageRequest? page = null) => await Read.QueryPageAsync(exp, page);
    public virtual async Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>>? exp, Expression<Func<T, E>>? orderBy, bool isAsc = true, PageRequest? page = null) => await Read.QueryPageAsync(exp, orderBy, isAsc, page);
    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>>? exp) => await Read.IsExistAsync(exp);
    public virtual async Task<int> UpdateAsync(T? entity) => await Write.UpdateAsync(entity);
    public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity) => await Write.UpdateAsync(where, entity);
    public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? exp) => await Read.QueryAsync(exp);
    public virtual async Task<IEnumerable<T>> QueryAsync(IQueryable<T>? query) => await Read.QueryAsync(query);

    [Obsolete("统一接口名称, 该方法将被弃用, 使用 QueryAsync 代替")]
    public virtual async Task<IEnumerable<T>> QueryDataAsync(IQueryable<T>? query)
    {
        return await Read.QueryDataAsync(query);
    }
    public virtual async Task<PageData<T>> QueryPageAsync(IQueryable<T>? query, PageRequest? page = null)
    {
        return await Read.QueryPageAsync(query, page);
    }

    public virtual async Task<PageData<ReturnT>> QueryPageAsync<ReturnT>(IQueryable<ReturnT>? query, PageRequest? page = null)
    {
        return await Read.QueryPageAsync(query, page);
    }
    public virtual async Task<int> UpdateWithoutTransactionAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity)
    {
        return await Write.UpdateWithoutTransactionAsync(where, entity);
    }
    public virtual async Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false) => await Write.DeleteAsync(id, isTrue);
    public virtual async Task<int> DeleteByIdsAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false) => await Write.DeleteByIdsAsync(id, isTrue);
    public virtual async Task<T?> GetByIdAsync<TKey>(TKey? id) => await Read.GetByIdAsync(id);
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids) => await Read.QueryByIdsAsync(ids);

    protected override void Dispose(bool disposing)
    {
        TransManager.Remove(_db);
        base.Dispose(disposing);
    }
}