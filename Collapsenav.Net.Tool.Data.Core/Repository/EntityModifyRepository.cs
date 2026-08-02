using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;

public partial class EntityModifyRepository<T> : ModifyRepository<T>, IEntityModifyRepository<T>
    where T : class, IEntity, new()
{
    public EntityModifyRepository(IDB db) : base(db) { }

    public override async Task<T> AddAsync(T? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        entity.Init();
        return await base.AddAsync(entity);
    }
    public override async Task<IEnumerable<T>> AddAsync(IEnumerable<T>? entityList)
    {
        if (entityList == null)
            throw new ArgumentNullException(nameof(entityList));
        foreach (var entity in entityList)
            entity.Init();
        return await base.AddAsync(entityList);
    }
    public override async Task<T> AddOrUpdateAsync(T? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        var (result, isNew) = await _db.AddOrUpdateAsync(entity);
        if (isNew) result.Init();
        else result.Update();
        return result;
    }
    public override async Task<T> UpdateAsync(T? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        entity.Update();
        return await base.UpdateAsync(entity);
    }
    public override async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T>? entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        foreach (var entity in entities)
            entity.Update();
        return await base.UpdateAsync(entities);
    }
    public override async Task<int> DeleteAsync(Expression<Func<T, bool>>? exp, bool isTrue = false)
    {
        if (exp == null)
            throw new ArgumentNullException(nameof(exp));
        if (isTrue)
            return await base.DeleteAsync(exp, true);
        var entities = _db.Set<T>().Where(exp).ToList();
        foreach (var entity in entities)
        {
            entity.SoftDelete();
            _db.Update(entity);
        }
        return entities.Count;
    }
}
