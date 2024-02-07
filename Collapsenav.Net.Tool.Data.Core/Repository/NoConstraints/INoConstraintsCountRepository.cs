using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsCountRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 统计数量
    /// </summary>
    /// <param name="exp">查询表达式</param>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<T, bool>>? exp = null);
}
public interface INoConstraintsCountRepository<Context, T> : INoConstraintsCountRepository<T> { }