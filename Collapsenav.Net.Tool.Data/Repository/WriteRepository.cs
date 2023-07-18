using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data;
public partial class WriteRepository<T> : Repository<T>, IWriteRepository<T>
    where T : class, IEntity, new()
{
    public WriteRepository(DbContext db) : base(db)
    {
    }
    /// <summary>
    /// 添加数据(单个)
    /// </summary>
    /// <param name="entity">新的数据</param>
    public virtual async Task<T?> AddAsync(T? entity)
    {
        if (entity == null)
            return null;
        entity.Init();
        await dbSet.AddAsync(entity);
        return entity;
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    public virtual async Task<int> UpdateAsync(T? entity)
    {
        // dbSet.Update(entity);
        if (entity == null)
            return 0;
        entity.Update();
        var entry = _db.Entry(entity);
        entry.State = EntityState.Modified;
        await Task.FromResult("");
        return 1;
    }
    /// <summary>
    /// 根据id删除数据
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <param name="isTrue">是否真删</param>
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
}
public partial class WriteRepository<TKey, T> : WriteRepository<T>, IWriteRepository<TKey, T>
    where T : class, IEntity<TKey>, new()
{
    public WriteRepository(DbContext db) : base(db) { }
    /// <summary>
    /// 根据id删除数据
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <param name="isTrue">是否真删</param>
    public virtual async Task<bool> DeleteAsync(TKey? id, bool isTrue = false)
    {
        if (id == null)
            return false;
        return await base.DeleteAsync(id, isTrue);
    }
}
