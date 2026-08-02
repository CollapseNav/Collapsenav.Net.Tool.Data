namespace Collapsenav.Net.Tool.Data;
public class EntityQueryRepository<T> : QueryRepository<T>, IEntityQueryRepository<T> where T : class, IEntity
{
    public EntityQueryRepository(IDB db) : base(db) { }
}