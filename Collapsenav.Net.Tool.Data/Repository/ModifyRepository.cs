using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data;
public partial class ModifyRepository<Context, T> : ModifyRepository<T>, IModifyRepository<Context, T>
    where T : class, IEntity, new() where Context : DbContext
{
    public ModifyRepository(Context db) : base(db) { }
}
public partial class ModifyRepository<T> : NoConstraintsModifyRepository<T>, IModifyRepository<T>
    where T : class, IEntity, new()
{
    public ModifyRepository(DbContext db) : base(db) { }

    public override async Task<int> AddAsync(IEnumerable<T>? entityList)
    {
        if (entityList == null)
            return 0;
        foreach (var entity in entityList)
            entity.Init();
        await dbSet.AddRangeAsync(entityList);
        return entityList.Count();
    }
    public override async Task<int> DeleteByIdsAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false)
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
    public override async Task<T?> AddAsync(T? entity)
    {
        if (entity == null)
            return null;
        entity.Init();
        await dbSet.AddAsync(entity);
        return entity;
    }
    public override async Task<int> UpdateAsync(T? entity)
    {
        if (entity == null)
            return 0;
        entity.Update();
        var entry = _db.Entry(entity);
        entry.State = EntityState.Modified;
        await Task.FromResult("");
        return 1;
    }
    public override async Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false) where TKey : default
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
}
