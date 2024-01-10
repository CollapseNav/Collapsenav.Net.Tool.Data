namespace Collapsenav.Net.Tool.Data;
public interface IQueryRepository<T> : INoConstraintsQueryRepository<T>, IRepository<T>, ICountRepository<T>, ICheckExistRepository<T> where T : class, IEntity { }
