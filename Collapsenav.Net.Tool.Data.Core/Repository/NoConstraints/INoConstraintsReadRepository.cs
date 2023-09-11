namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsReadRepository<T> : INoConstraintsRepository<T>, INoConstraintsCountRepository<T>, INoConstraintsCheckExistRepository<T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync<TKey>(TKey id);
    [Obsolete("统一接口名称, 该方法将被弃用, 使用 QueryAsync 代替")]
    Task<IEnumerable<T>> QueryDataAsync(IQueryable<T>? query);
    /// <summary>
    /// 根据query进行查询，与直接ToList没啥区别
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> QueryAsync(IQueryable<T>? query);
    /// <summary>
    /// 根据query进行分页查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="page">分页参数</param>
    /// <returns></returns>
    Task<PageData<T>> QueryPageAsync(IQueryable<T>? query, PageRequest? page = null);
    /// <summary>
    /// 根据query进行分页查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="page">分页参数</param>
    /// <typeparam name="ReturnT"></typeparam>
    /// <returns></returns>
    Task<PageData<ReturnT>> QueryPageAsync<ReturnT>(IQueryable<ReturnT>? query, PageRequest? page = null);
}
