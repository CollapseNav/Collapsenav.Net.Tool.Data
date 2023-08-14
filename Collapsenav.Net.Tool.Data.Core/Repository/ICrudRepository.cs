namespace Collapsenav.Net.Tool.Data;

public interface ICrudRepository<T> : INoConstraintsCrudRepository<T>, IModifyRepository<T>, IQueryRepository<T> where T : class, IEntity
{
}
public interface ICrudRepository<TKey, T> : INoConstraintsCrudRepository<TKey, T>, ICrudRepository<T>, IModifyRepository<TKey, T>, IQueryRepository<TKey, T> where T : class, IEntity<TKey>
{
}