namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 查询仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IQueryRepository<T> : INoConstraintsQueryRepository<T>, IRepository<T> where T : class, IEntity { }
