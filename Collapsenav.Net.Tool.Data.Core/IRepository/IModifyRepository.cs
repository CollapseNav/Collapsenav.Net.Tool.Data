namespace Collapsenav.Net.Tool.Data;
/// <summary>
/// 修改仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IModifyRepository<T> : INoConstraintsModifyRepository<T>, IRepository<T> where T : class, IEntity { }