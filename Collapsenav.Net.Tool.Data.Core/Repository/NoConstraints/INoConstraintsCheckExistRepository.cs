using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsCheckExistRepository<T> : INoConstraintsRepository<T> where T : class
{
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="exp">查询表达式</param>
    /// <returns></returns>
    Task<bool> IsExistAsync(Expression<Func<T, bool>>? exp);
}
public interface INoConstraintsCheckExistRepository<Context, T> : INoConstraintsCheckExistRepository<T> where T : class { }