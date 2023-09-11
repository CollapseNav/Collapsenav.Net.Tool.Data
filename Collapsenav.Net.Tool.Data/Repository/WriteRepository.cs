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
    public virtual async Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false)
    {
        if (id == null)
            return false;
        var entity = await dbSet.FindAsync(GetKeyValue(id));
        if (entity == null)
            return false;
        if (isTrue)
        {
            var result = dbSet.Remove(entity);
            return result.State == EntityState.Deleted;
        }
        else
        {
            entity.SoftDelete();
            return (await UpdateAsync(entity)) > 0;
        }
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
