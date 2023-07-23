using System.Linq.Expressions;
using System.Reflection;

namespace Collapsenav.Net.Tool.Data;
public interface IRepository : IDisposable
{
    IQueryable<E> Query<E>(Expression<Func<E, bool>>? exp = null) where E : class;
    IQueryable<E> QueryWithTrack<E>(Expression<Func<E, bool>>? exp = null) where E : class;
}
public interface IRepository<T> : IRepository where T : IEntity
{
    /// <summary>
    /// 获取 Query
    /// </summary>
    IQueryable<T> Query(Expression<Func<T, bool>>? exp = null);
    IQueryable<T> QueryWithTrack(Expression<Func<T, bool>>? exp = null);
    /// <summary>
    /// 保存
    /// </summary>
    Task<int> SaveAsync();
    /// <summary>
    /// 保存
    /// </summary>
    int Save();
    /// <summary>
    /// 获取主键type
    /// </summary>
    Type? KeyType();
    PropertyInfo? KeyProp();
}
public interface IRepository<TKey, T> : IRepository<T> where T : IEntity<TKey>
{
}

