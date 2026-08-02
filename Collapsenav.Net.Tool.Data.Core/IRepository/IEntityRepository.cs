namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 约束版基础仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityRepository<T> : IRepository<T> where T : class, IEntity { }
