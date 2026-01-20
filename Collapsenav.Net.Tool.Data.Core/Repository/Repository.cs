namespace Collapsenav.Net.Tool.Data;
public class Repository<T> : NoConstraintsRepository<T>, IRepository<T> where T : class, IEntity
{
    public Repository(IDB db) : base(db) { }
}
