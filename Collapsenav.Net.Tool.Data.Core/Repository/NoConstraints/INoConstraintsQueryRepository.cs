using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsQueryRepository<T> : INoConstraintsReadRepository<T>
{
    /// <summary>
    /// 列表查询
    /// </summary>
    /// <param name="exp">查询表达式</param>
    /// <returns></returns>
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? exp = null);
    /// <summary>
    /// 根据Id集合查询
    /// </summary>
    /// <param name="ids">主键集合</param>
    /// <returns></returns>
    Task<IEnumerable<T>> QueryByIdsAsync<TKey>(IEnumerable<TKey>? ids);
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="exp">查询表达式</param>
    /// <param name="page">分页参数</param>
    /// <returns></returns>
    Task<PageData<T>> QueryPageAsync(Expression<Func<T, bool>>? exp, PageRequest? page = null);
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="exp">查询表达式</param>
    /// <param name="orderBy">排序</param>
    /// <param name="isAsc">是否正序</param>
    /// <param name="page">分页参数</param>
    /// <typeparam name="E"></typeparam>
    /// <returns></returns>
    /// TODO 后续说不定可以将排序等参数与分页参数合并
    Task<PageData<T>> QueryPageAsync<E>(Expression<Func<T, bool>>? exp, Expression<Func<T, E>>? orderBy, bool isAsc = true, PageRequest? page = null);
}