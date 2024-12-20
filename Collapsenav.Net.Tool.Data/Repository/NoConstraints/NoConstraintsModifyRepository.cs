using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data;
public partial class NoConstraintsModifyRepository<Context, T> : NoConstraintsModifyRepository<T>, INoConstraintsModifyRepository<Context, T>
    where T : class, new() where Context : DbContext
{
    public NoConstraintsModifyRepository(Context db) : base(db) { }
}
public partial class NoConstraintsModifyRepository<T> : NoConstraintsRepository<T>, INoConstraintsModifyRepository<T>
    where T : class, new()
{
    public NoConstraintsModifyRepository(DbContext db) : base(db)
    {
        TransManager.Add(db);
    }

    public virtual async Task<int> AddAsync(IEnumerable<T>? entityList)
    {
        if (entityList == null)
            return 0;
        await dbSet.AddRangeAsync(entityList);
        return entityList.Count();
    }
    public virtual async Task<T?> AddOrUpdateAsync(T? entity)
    {
        if (entity == null)
            return null;
        var existedValue = await dbSet.FindAsync(GetKeyValue(KeyProp().GetValue(entity)));
        if (existedValue == null)
            return await AddAsync(entity);
        else
        {
            var entry = _db.Entry(existedValue);
            entry.State = EntityState.Detached;
            await UpdateAsync(entity);
            return entity;
        }
    }
    public virtual async Task<int> DeleteByIdsAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false)
    {
        if (id == null)
            return 0;
        TransManager.CreateTranscation(_db);
        var entitys = id.Select(item => dbSet.Find(GetKeyValue(item!))).Where(item => item! != null).Select(item => item!);
        if (isTrue)
        {
            entitys.ForEach(item => dbSet.Remove(item));
            return entitys.Count();
        }
        return await Task.Factory.StartNew(() =>
        {
            var matches = id.Select(item => dbSet.Find(GetKeyValue(item!))).Where(item => item! != null).Select(item => item!);
            matches.ForEach(item =>
            {
                dbSet.Update(item);
            });
            return matches.Count();
        });
    }
    public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>>? exp, bool isTrue = false)
    {
        if (exp == null)
            return 0;
        TransManager.CreateTranscation(_db);
        // TODO 处理软删除的情况
        if (isTrue)
        {
            return await dbSet.Where(exp).DeleteAsync();
        }
        else
        {
            var type = typeof(T);
            if (type.IsType<BaseEntity>())
            {
                return await dbSet.Where(exp).UpdateAsync(item => new
                {
                    IsDeleted = true,
                    LastModificationTime = DateTime.Now,
                });
            }
        }
        return 0;
    }
    public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity)
    {
        TransManager.CreateTranscation(_db);
        return where == null ? 0 : await dbSet.Where(where).UpdateAsync(entity);
    }

    public virtual async Task<int> UpdateWithoutTransactionAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity)
    {
        return where == null ? 0 : await dbSet.Where(where).UpdateAsync(entity);
    }
    public virtual async Task<T?> AddAsync(T? entity)
    {
        if (entity == null)
            return null;
        await dbSet.AddAsync(entity);
        return entity;
    }
    public virtual async Task<int> UpdateAsync(T? entity)
    {
        if (entity == null)
            return 0;
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
