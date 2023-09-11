namespace Collapsenav.Net.Tool.Data;
public interface IWriteRepository<T> : INoConstraintsWriteRepository<T>, IRepository<T> where T : class, IEntity { }
