using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsModifyRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T?> AddAsync(T? entity);
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entityList"></param>
    /// <returns></returns>
    Task<int> AddAsync(IEnumerable<T>? entityList);
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="exp">删除用的查询表达式</param>
    /// <param name="isTrue">是否物理删除</param>
    /// <returns></returns>
    Task<int> DeleteAsync(Expression<Func<T, bool>>? exp, bool isTrue = false);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">需要删除的数据id</param>
    /// <param name="isTrue">是否物理删除，默认为逻辑删除/软删除</param>
    /// <returns></returns>
    Task<bool> DeleteAsync<TKey>(TKey? id, bool isTrue = false);
    /// <summary>
    /// 根据id批量删除
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isTrue">是否物理删除</param>
    /// <returns></returns>
    Task<int> DeleteByIdsAsync<TKey>(IEnumerable<TKey>? id, bool isTrue = false);
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">主键有值的实体</param>
    /// <returns></returns>
    Task<int> UpdateAsync(T? entity);
    /// <summary>
    /// 批量更新(默认带事务)
    /// </summary>
    /// <param name="where">匹配的查询表达式</param>
    /// <param name="entity">更新用的表达式</param>
    /// <returns></returns>
    Task<int> UpdateAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity);
    /// <summary>
    /// 批量更新(无事务直接更新)
    /// </summary>
    /// <param name="where">匹配的查询表达式</param>
    /// <param name="entity">更新用的表达式</param>
    /// <returns></returns>
    Task<int> UpdateWithoutTransactionAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity);
}