namespace Collapsenav.Net.Tool.Data;
public class EntityCrudRepository<T> : CrudRepository<T>, IEntityCrudRepository<T> where T : class, IEntity, new()
{
    public EntityCrudRepository(IEntityQueryRepository<T> read, IEntityModifyRepository<T> write, IDB db) : base(read, write, db) { }
}