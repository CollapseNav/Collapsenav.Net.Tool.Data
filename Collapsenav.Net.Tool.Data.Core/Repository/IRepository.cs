using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
public interface IRepository : IDisposable
{
    /// <summary>
    /// 获取 E 类型的 queryable
    /// </summary>
    /// <typeparam name="E"></typeparam>
    IQueryable<E> Query<E>(Expression<Func<E, bool>>? exp = null) where E : class;
    /// <summary>
    /// 获取 E 类型的 queryable，带跟踪track
    /// </summary>
    /// <typeparam name="E"></typeparam>
    IQueryable<E> QueryWithTrack<E>(Expression<Func<E, bool>>? exp = null) where E : class;
}
public interface IRepository<T> : INoConstraintsRepository<T>, IRepository where T : IEntity
{
}
public interface IRepository<TKey, T> : INoConstraintsRepository<TKey, T>, IRepository<T> where T : IEntity<TKey>
{
}

