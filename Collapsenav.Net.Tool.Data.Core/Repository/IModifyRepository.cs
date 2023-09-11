namespace Collapsenav.Net.Tool.Data;
public interface IModifyRepository<T> : INoConstraintsModifyRepository<T>, IWriteRepository<T> where T : class, IEntity { }