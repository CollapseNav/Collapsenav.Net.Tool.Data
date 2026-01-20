using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;

public class NoConstraintsCrudRepository<T> : NoConstraintsRepository<T>, INoConstraintsCrudRepository<T> where T : class, new()
{
    protected readonly INoConstraintsQueryRepository<T> Read;
    protected readonly INoConstraintsModifyRepository<T> Write;
    public NoConstraintsCrudRepository(INoConstraintsQueryRepository<T> read, INoConstraintsModifyRepository<T> write, IDB db) : base(db)
    {
        (Read, Write) = (read, write);
    }
    public virtual Task<T> AddAsync(T? entity) => Write.AddAsync(entity);
    public virtual Task<IEnumerable<T>> AddAsync(IEnumerable<T>? entityList) => _db.AddAsync(entityList);
    public virtual Task<T> AddOrUpdateAsync(T? entity) => Write.AddOrUpdateAsync(entity);
    public virtual Task<int> DeleteAsync(Expression<Func<T, bool>>? exp, bool isTrue = false) => Write.DeleteAsync(exp, isTrue);
    public virtual Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>>? exp, PageRequest? page = null) => Read.QueryPageAsync(exp, page);
    public virtual Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>>? exp, Expression<Func<T, E>>? orderBy, bool isAsc = true, PageRequest? page = null) => Read.QueryPageAsync(exp, orderBy, isAsc, page);
    public virtual Task<T> UpdateAsync(T? entity) => Write.UpdateAsync(entity);
    public virtual Task<int> DeleteAsync<TKey>(TKey? id, bool isTrue = false) => Write.DeleteAsync(id, isTrue);
    public virtual Task<int> DeleteByIdsAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false) => Write.DeleteByIdsAsync(id, isTrue);
    public virtual Task<T?> GetByIdAsync<TKey>(TKey? id) => Read.GetByIdAsync(id);
    public virtual Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids) => Read.QueryByIdsAsync(ids);
    public virtual Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? exp = null) => Read.QueryAsync(exp);
    public Task<int> UpdateAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity) => Write.UpdateAsync(where, entity);
    protected override void Dispose(bool disposing)
    {
        // TransManager.Remove(_db);
        base.Dispose(disposing);
    }

    public Task<IEnumerable<T>> UpdateAsync(IEnumerable<T>? entity)
    {
        return Write.UpdateAsync(entity);
    }

}