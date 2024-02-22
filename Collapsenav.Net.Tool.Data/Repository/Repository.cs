using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;
public class Repository<Context, T> : Repository<T>, IRepository<Context, T> where T : class, IEntity where Context : DbContext
{
    public Repository(Context db) : base(db) { }
}
public class Repository<T> : NoConstraintsRepository<T>, IRepository<T> where T : class, IEntity
{
    public Repository(DbContext db) : base(db) { }
}
