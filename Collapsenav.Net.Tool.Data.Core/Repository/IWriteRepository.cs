namespace Collapsenav.Net.Tool.Data;

public interface IWriteRepository<T> : INoConstraintsWriteRepository<T>, IRepository<T> where T : class, IEntity
{
}
public interface IWriteRepository<TKey, T> : INoConstraintsWriteRepository<TKey, T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
}

