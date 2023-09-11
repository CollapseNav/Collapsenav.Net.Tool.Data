namespace Collapsenav.Net.Tool.Data;
public interface IReadRepository<T> : INoConstraintsReadRepository<T>, IRepository<T>, ICountRepository<T>, ICheckExistRepository<T> where T : class, IEntity { }