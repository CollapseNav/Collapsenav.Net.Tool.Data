using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data;
public partial class ModifyRepository<T> : WriteRepository<T>, IModifyRepository<T>
    where T : class, IEntity, new()
{
    public ModifyRepository(DbContext db) : base(db)
    {
    }
    public virtual async Task<int> AddAsync(IEnumerable<T>? entityList)
    {
        if (entityList == null)
            return 0;
        foreach (var entity in entityList)
            entity.Init();
        await dbSet.AddRangeAsync(entityList);
        return entityList.Count();
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
}
public partial class ModifyRepository<TKey, T> : ModifyRepository<T>, IModifyRepository<TKey, T>
    where T : class, IEntity<TKey>, new()
{
    public ModifyRepository(DbContext db) : base(db) { }
    public virtual async Task<int> DeleteAsync(IEnumerable<TKey>? id, bool isTrue = false)
    {
        if (id == null)
            return 0;
        TransManager.CreateTranscation(_db);
        if (isTrue)
            return await dbSet.Where(item => id.Contains(item.Id)).DeleteAsync();
        return await dbSet.Where(item => id.Contains(item.Id)).UpdateAsync(entity => new
        {
            LastModificationTime = DateTime.Now,
            IsDeleted = true
        });
    }

    public virtual async Task<bool> DeleteAsync(TKey? id, bool isTrue = false)
    {
        return await base.DeleteAsync(id, isTrue);
    }
}
