using System.Linq.Expressions;
using System.Reflection;
namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 无泛型约束的基础仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INoConstraintsRepository<T> : IRepository
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
}