using System.Linq.Expressions;
using System.Reflection;
namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 无泛型约束的基础仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INoConstraintsRepository<T> : IRepository where T : class
{
    /// <summary>
    /// 获取 Query
    /// </summary>
    /// <param name="exp">筛选条件
    /// PS:若使用默认的NULL，则返回所有数据
    /// </param>
    IQueryable<T> Query(Expression<Func<T, bool>>? exp = null);
    /// <summary>
    /// 获取带ef跟踪的query
    /// </summary>
    /// <param name="exp">筛选条件
    /// PS:若使用默认的NULL，则返回所有数据
    /// </param>
    IQueryable<T> QueryWithTrack(Expression<Func<T, bool>>? exp = null);
    /// <summary>
    /// 异步保存
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
    /// <summary>
    /// 主键属性
    /// </summary>
    PropertyInfo? KeyProp();
    /// <summary>
    /// 提供的LeftJoin方法，用于创建GroupJoinResult以支持更多的Join
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    /// <param name="LKey"></param>
    /// <param name="RKey"></param>
    GroupJoinResult<T, T2> LeftJoin<T2>(Expression<Func<T, object?>> LKey, Expression<Func<T2, object?>> RKey) where T2 : class;
    /// <summary>
    /// 提供的Join方法，用于创建GroupJoinResult以支持更多的Join
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    /// <param name="LKey"></param>
    /// <param name="RKey"></param>
    GroupJoinResult<T, T2> Join<T2>(Expression<Func<T, object?>> LKey, Expression<Func<T2, object?>> RKey) where T2 : class;
}
public interface INoConstraintsRepository<Context, T> : IRepository where T : class { }
