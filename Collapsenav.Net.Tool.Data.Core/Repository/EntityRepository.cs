namespace Collapsenav.Net.Tool.Data;
public class EntityRepository<T> : Repository<T>, IEntityRepository<T> where T : class, IEntity
{
    public EntityRepository(IDB db) : base(db) { }
}
