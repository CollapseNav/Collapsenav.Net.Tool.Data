namespace Collapsenav.Net.Tool.Data;

public interface IReadRepository<T> : INoConstraintsReadRepository<T>, IRepository<T>, ICountRepository<T>, ICheckExistRepository<T> where T : class, IEntity
{
}
public interface IReadRepository<TKey, T> : INoConstraintsReadRepository<TKey, T>, IReadRepository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
}
