namespace Collapsenav.Net.Tool.Data;
public class CrudRepository<T> : NoConstraintsCrudRepository<T>, ICrudRepository<T> where T : class, IEntity, new()
{
    public CrudRepository(IQueryRepository<T> read, IModifyRepository<T> write, IDB db) : base(read, write, db) { }
}