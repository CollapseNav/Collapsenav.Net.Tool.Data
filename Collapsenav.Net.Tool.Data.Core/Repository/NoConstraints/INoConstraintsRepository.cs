using System.Linq.Expressions;
using System.Reflection;
namespace Collapsenav.Net.Tool.Data;
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
    /// <returns></returns>
    Task<int> SaveAsync();
    /// <summary>
    /// 保存
    /// </summary>
    /// <returns></returns>
    int Save();
    /// <summary>
    /// 获取主键type
    /// </summary>
    /// <returns></returns>
    Type? KeyType();
    /// <summary>
    /// 主键属性
    /// </summary>
    /// <returns></returns>
    PropertyInfo? KeyProp();
}