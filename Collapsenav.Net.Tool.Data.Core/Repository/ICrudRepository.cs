namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 增删改查仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrudRepository<T> : INoConstraintsCrudRepository<T>, IModifyRepository<T>, IQueryRepository<T> where T : class, IEntity { }
public interface ICrudRepository<Context, T> : ICrudRepository<T> where T : class, IEntity { }
public interface ICrudRepository<Read, Write, T> : ICrudRepository<T> where T : class, IEntity { }