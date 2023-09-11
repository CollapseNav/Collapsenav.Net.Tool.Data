namespace Collapsenav.Net.Tool.Data;
public interface ICrudRepository<T> : INoConstraintsCrudRepository<T>, IModifyRepository<T>, IQueryRepository<T> where T : class, IEntity { }