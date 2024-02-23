using Microsoft.EntityFrameworkCore;
namespace Collapsenav.Net.Tool.Data;
public class CrudRepository<Read, Wirte, T> : CrudRepository<T>, ICrudRepository<Read, Wirte, T>
    where T : class, IEntity, new() where Read : ReadContext where Wirte : WriteContext
{
    public CrudRepository(IQueryRepository<Read, T> read, IModifyRepository<Wirte, T> write, Read db) : base(read, write, db) { }
}
public class CrudRepository<Context, T> : CrudRepository<T>, ICrudRepository<Context, T> where T : class, IEntity, new()
        where Context : DbContext
{
    public CrudRepository(IQueryRepository<Context, T> read, IModifyRepository<Context, T> write, Context db) : base(read, write, db) { }
}
public class CrudRepository<T> : NoConstraintsCrudRepository<T>, ICrudRepository<T> where T : class, IEntity, new()
{
    public CrudRepository(IQueryRepository<T> read, IModifyRepository<T> write, DbContext db) : base(read, write, db) { }
}