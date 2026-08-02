namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 约束版修改仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityModifyRepository<T> : IModifyRepository<T>, IEntityRepository<T> where T : class, IEntity { }