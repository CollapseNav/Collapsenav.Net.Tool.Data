using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;
public partial class WriteRepository<T> : Repository<T>, IWriteRepository<T>
    where T : class, IEntity, new()
{
    public WriteRepository(DbContext db) : base(db)
    {
        TransManager.Add(db);
    }
    public virtual async Task<T?> AddAsync(T? entity)
    {
        if (entity == null)
            return null;
        entity.Init();
        await dbSet.AddAsync(entity);
        return entity;
    }
    public virtual async Task<int> UpdateAsync(T? entity)
    {
        if (entity == null)
            return 0;
        entity.Update();
        var entry = _db.Entry(entity);
        entry.State = EntityState.Modified;
        await Task.FromResult("");
        return 1;
    }
    public virtual async Task<bool> DeleteAsync(object? id, bool isTrue = false)
    {
        var entity = KeyType().Name switch
        {
            nameof(Int32) => await dbSet.FindAsync(int.Parse(id?.ToString() ?? string.Empty)),
            nameof(Int64) => await dbSet.FindAsync(long.Parse(id?.ToString() ?? string.Empty)),
            nameof(String) => await dbSet.FindAsync(id?.ToString() ?? string.Empty),
            nameof(Guid) => await dbSet.FindAsync(Guid.Parse(id?.ToString() ?? string.Empty)),
            _ => null,
        };
        if (entity == null)
            return false;
        if (isTrue)
            dbSet.Remove(entity);
        else
        {
            entity.SoftDelete();
            await UpdateAsync(entity);
        }
        return true;
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                TransManager.Remove(_db);
            }
            disposedValue = true;
        }
    }
}
public partial class WriteRepository<TKey, T> : WriteRepository<T>, IWriteRepository<TKey, T>
    where T : class, IEntity<TKey>, new()
{
    public WriteRepository(DbContext db) : base(db) { }
    public virtual async Task<bool> DeleteAsync(TKey? id, bool isTrue = false)
    {
        if (id == null)
            return false;
        return await base.DeleteAsync(id, isTrue);
    }
}
