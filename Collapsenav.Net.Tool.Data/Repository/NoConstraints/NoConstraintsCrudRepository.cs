using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class NoConstraintsCrudRepository<Read, Wirte, T> : NoConstraintsCrudRepository<T>, INoConstraintsCrudRepository<Read, Wirte, T>
    where T : class, new() where Read : ReadContext where Wirte : WriteContext
{
    public NoConstraintsCrudRepository(INoConstraintsQueryRepository<Read, T> read, INoConstraintsModifyRepository<Wirte, T> write, Read db) : base(read, write, db) { }
}
public class NoConstraintsCrudRepository<Context, T> : NoConstraintsCrudRepository<T>, INoConstraintsCrudRepository<Context, T>
        where T : class, new() where Context : DbContext
{
    public NoConstraintsCrudRepository(INoConstraintsQueryRepository<Context, T> read, INoConstraintsModifyRepository<Context, T> write, Context db) : base(read, write, db) { }
}
public class NoConstraintsCrudRepository<T> : NoConstraintsRepository<T>, INoConstraintsCrudRepository<T> where T : class, new()
{
    protected readonly INoConstraintsQueryRepository<T> Read;
    protected readonly INoConstraintsModifyRepository<T> Write;
    public NoConstraintsCrudRepository(INoConstraintsQueryRepository<T> read, INoConstraintsModifyRepository<T> write, DbContext db) : base(db)
    {
        (Read, Write) = (read, write);
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

    public virtual async Task<IEnumerable<E>> QueryAsync<E>(IQueryable<E>? query)
    {
        return await Read.QueryAsync(query);
    }
}