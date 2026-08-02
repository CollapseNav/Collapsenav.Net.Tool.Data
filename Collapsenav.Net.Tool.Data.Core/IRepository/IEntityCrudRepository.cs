namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 约束版增删改查仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityCrudRepository<T> : ICrudRepository<T>, IEntityModifyRepository<T>, IEntityQueryRepository<T> where T : class, IEntity { }