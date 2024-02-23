namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 无泛型约束的增删查改仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INoConstraintsCrudRepository<T> : INoConstraintsModifyRepository<T>, INoConstraintsQueryRepository<T> where T : class { }
public interface INoConstraintsCrudRepository<Context, T> : INoConstraintsCrudRepository<T> where T : class { }
public interface INoConstraintsCrudRepository<Read, Write, T> : INoConstraintsCrudRepository<T> where T : class { }
