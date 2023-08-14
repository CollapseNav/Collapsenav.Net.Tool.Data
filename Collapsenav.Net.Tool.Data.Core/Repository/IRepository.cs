using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
public interface IRepository : IDisposable
{
    IQueryable<E> Query<E>(Expression<Func<E, bool>>? exp = null) where E : class;
    IQueryable<E> QueryWithTrack<E>(Expression<Func<E, bool>>? exp = null) where E : class;
}
public interface IRepository<T> : INoConstraintsRepository<T>, IRepository where T : IEntity
{
}
public interface IRepository<TKey, T> : INoConstraintsRepository<TKey, T>, IRepository<T> where T : IEntity<TKey>
{
}

