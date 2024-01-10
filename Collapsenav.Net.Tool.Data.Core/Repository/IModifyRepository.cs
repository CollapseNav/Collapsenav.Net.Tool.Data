namespace Collapsenav.Net.Tool.Data;
public interface IModifyRepository<T> : INoConstraintsModifyRepository<T>, IRepository<T> where T : class, IEntity { }