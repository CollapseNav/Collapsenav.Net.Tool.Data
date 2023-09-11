using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data;
public partial class ModifyRepository<T> : WriteRepository<T>, IModifyRepository<T>
    where T : class, IEntity, new()
{
    public ModifyRepository(DbContext db) : base(db) { }
    public virtual async Task<int> AddAsync(IEnumerable<T>? entityList)
    {
        if (entityList == null)
            return 0;
        foreach (var entity in entityList)
            entity.Init();
        await dbSet.AddRangeAsync(entityList);
        return entityList.Count();
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
                item.SoftDelete();
                item.Update();
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
}
