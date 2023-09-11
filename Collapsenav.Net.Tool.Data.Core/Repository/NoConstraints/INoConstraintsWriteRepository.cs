namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsWriteRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T?> AddAsync(T? entity);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">需要删除的数据id</param>
    /// <param name="isTrue">是否物理删除，默认为逻辑删除/软删除</param>
    /// <returns></returns>
    Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">主键有值的实体</param>
    /// <returns></returns>
    Task<int> UpdateAsync(T? entity);
}