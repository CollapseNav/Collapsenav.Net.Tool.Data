namespace Collapsenav.Net.Tool.Data;

public interface IModifyRepository<T> : INoConstraintsModifyRepository<T>, IWriteRepository<T> where T : class, IEntity
{
}
public interface IModifyRepository<TKey, T> : INoConstraintsModifyRepository<TKey, T>, IModifyRepository<T>, IWriteRepository<TKey, T> where T : class, IEntity<TKey>
{
}