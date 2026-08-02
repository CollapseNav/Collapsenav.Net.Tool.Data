namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 约束版查询仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityQueryRepository<T> : IQueryRepository<T>, IEntityRepository<T> where T : class, IEntity { }
