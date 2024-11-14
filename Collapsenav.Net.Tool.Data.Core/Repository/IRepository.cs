using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 基础仓储
/// </summary>
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
/// <summary>
/// 基础仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> : INoConstraintsRepository<T> where T : class, IEntity { }
public interface IRepository<Context, T> : IRepository<T> where T : class, IEntity { }
