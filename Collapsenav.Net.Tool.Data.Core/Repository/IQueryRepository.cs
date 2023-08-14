namespace Collapsenav.Net.Tool.Data;

public interface IQueryRepository<T> : INoConstraintsQueryRepository<T>, IReadRepository<T> where T : class, IEntity
{
}
public interface IQueryRepository<TKey, T> : INoConstraintsQueryRepository<TKey, T>, IQueryRepository<T>, IReadRepository<TKey, T> where T : class, IEntity<TKey>
{
}
