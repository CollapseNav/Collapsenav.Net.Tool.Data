namespace Collapsenav.Net.Tool.Data;
public interface ICheckExistRepository<T> : INoConstraintsCheckExistRepository<T>, IRepository<T> where T : class, IEntity { }
public interface ICheckExistRepository<Context, T> : ICheckExistRepository<T> where T : class, IEntity { }
