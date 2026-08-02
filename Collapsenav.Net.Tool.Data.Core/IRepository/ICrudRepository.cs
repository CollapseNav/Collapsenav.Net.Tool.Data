namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 无约束增删查改仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrudRepository<T> : IModifyRepository<T>, IQueryRepository<T> where T : class { }
