namespace Collapsenav.Net.Tool.Data;
public class QueryRepository<T> : NoConstraintsQueryRepository<T>, IQueryRepository<T> where T : class, IEntity
{
    public QueryRepository(IDB db) : base(db) { }
}