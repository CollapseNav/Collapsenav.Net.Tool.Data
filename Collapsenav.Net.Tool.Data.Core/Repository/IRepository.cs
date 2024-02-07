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
public interface IRepository<T> : INoConstraintsRepository<T> where T : IEntity
{
    /// <summary>
    /// 提供的LeftJoin方法，用于创建GroupJoinResult以支持更多的Join
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    /// <param name="LKey"></param>
    /// <param name="RKey"></param>
    GroupJoinResult<T, T2> LeftJoin<T2>(Expression<Func<T, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity;
    /// <summary>
    /// 提供的Join方法，用于创建GroupJoinResult以支持更多的Join
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    /// <param name="LKey"></param>
    /// <param name="RKey"></param>
    GroupJoinResult<T, T2> Join<T2>(Expression<Func<T, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity;
}
