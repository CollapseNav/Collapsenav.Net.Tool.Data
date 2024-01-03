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
    GroupJoinResult<T, T2> LeftJoin<T2>(Expression<Func<T, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity;
    GroupJoinResult<T, T2> Join<T2>(Expression<Func<T, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity;
}
