namespace Collapsenav.Net.Tool.Data;
public interface ICheckExistRepository<T> : INoConstraintsCheckExistRepository<T>, IRepository<T> where T : class, IEntity { }
