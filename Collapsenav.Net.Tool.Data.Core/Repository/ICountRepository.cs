namespace Collapsenav.Net.Tool.Data;
public interface ICountRepository<T> : INoConstraintsCountRepository<T>, IRepository<T> where T : class, IEntity { }

