using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;

public partial class NoConstraintsModifyRepository<T> : NoConstraintsRepository<T>, INoConstraintsModifyRepository<T>
    where T : class
{
    public NoConstraintsModifyRepository(IDB db) : base(db)
    {
    }
    public virtual Task<IEnumerable<T>> AddAsync(IEnumerable<T>? entityList) => _db.AddAsync(entityList);
    public virtual Task<T> AddOrUpdateAsync(T? entity) => _db.AddOrUpdateAsync(entity);
    public virtual Task<int> DeleteByIdsAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false) => _db.DeleteByIdsAsync<T, TKey>(id, isTrue);
    public virtual Task<int> DeleteAsync(Expression<Func<T, bool>>? exp, bool isTrue = false) => _db.DeleteAsync(exp, isTrue);
    public virtual Task<T> AddAsync(T? entity) => _db.AddAsync(entity);
    public virtual Task<T> UpdateAsync(T? entity) => _db.UpdateAsync(entity);
    public virtual Task<IEnumerable<T>> UpdateAsync(IEnumerable<T>? entity) => _db.UpdateAsync(entity);
    public virtual Task<int> DeleteAsync<TKey>(TKey? id, bool isTrue = false) => _db.DeleteByIdAsync<T, TKey>(id, isTrue);
    public Task<int> UpdateAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity) => _db.UpdateAsync(where, entity);
    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TransManager.Remove(_db);
            }
            disposedValue = true;
        }
    }
}
