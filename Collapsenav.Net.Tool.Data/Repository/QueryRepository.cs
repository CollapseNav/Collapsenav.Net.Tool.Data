using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class QueryRepository<Context, T> : QueryRepository<T>, IQueryRepository<Context, T> where T : class, IEntity where Context : DbContext
{
    public QueryRepository(Context db) : base(db) { }
}
public class QueryRepository<T> : NoConstraintsQueryRepository<T>, IQueryRepository<T> where T : class, IEntity
{
    public QueryRepository(DbContext db) : base(db) { }
}