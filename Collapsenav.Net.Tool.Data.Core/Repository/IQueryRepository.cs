namespace Collapsenav.Net.Tool.Data;
public interface IQueryRepository<T> : INoConstraintsQueryRepository<T>, IReadRepository<T> where T : class, IEntity { }
